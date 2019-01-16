﻿using System;
using System.Collections.Generic;
using System.Reflection;

using TaskBuilder.Attributes;
using TaskBuilder.Functions;
using TaskBuilder.Models;
using TaskBuilder.Models.Function;
using TaskBuilder.Models.Function.Exceptions;

namespace TaskBuilder.Services
{
    public class FunctionModelBuilder : IFunctionModelBuilder
    {
        private readonly IDisplayConverter _displayConverter;
        private readonly IInputValueService _inputValueService;

        public FunctionModelBuilder(IDisplayConverter displayConverter, IInputValueService inputValueService)
        {
            _displayConverter = displayConverter;
            _inputValueService = inputValueService;
        }

        public IFunctionModel BuildSimpleFunctionModel(string name)
        {
            return new FunctionModel(name);
        }

        public IFunctionModel BuildFunctionModel(Type functionType)
        {
            var attribute = functionType.GetCustomAttribute<FunctionAttribute>();

            var functionModel = new FunctionModel(
                name: functionType.FullName,
                displayName: _displayConverter.GetDisplayName(attribute.DisplayName, functionType.FullName, functionType.Name),
                displayColor: _displayConverter.GetDisplayColor(null, attribute.DisplayColor)
                );

            IInvokeModel invokeModel;

            foreach (var method in functionType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (TryBuildInvokeModel(method, out invokeModel))
                {
                    functionModel.Invoke = invokeModel;
                    break;
                }
            }

            if (functionModel.Invoke == null)
                throw new MissingMethodException($"The type {nameof(functionType)} does not have a single public method named {nameof(IInvokable.Invoke)}. This is required for the function to be invoked.");

            IDispatchModel dispatchModel;
            bool hasDispatch = false;

            foreach (var property in functionType.GetProperties())
            {
                if (!hasDispatch && TryBuildDispatchModel(property, out dispatchModel))
                {
                    functionModel.Dispatch = dispatchModel;
                    hasDispatch = true;
                    continue;
                }

                IInputModel inputModel;

                if (TryBuildInputModel(property, functionType.FullName, out inputModel))
                {
                    functionModel.Inputs.Add(inputModel);
                    continue;
                }

                IOutputModel outputModel;

                if (TryBuildOutputModel(property, functionType.FullName, out outputModel))
                {
                    functionModel.Outputs.Add(outputModel);
                    continue;
                }
            }

            return functionModel;
        }

        public bool TryBuildInvokeModel(MethodInfo invokeMethod, out IInvokeModel invokeModel)
        {
            if (!invokeMethod.Name.Equals(nameof(IInvokable.Invoke), StringComparison.OrdinalIgnoreCase))
            {
                invokeModel = null;
                return false;
            }

            EnsureMemberType(invokeMethod.ReturnType != typeof(void), invokeMethod.Name, "void", nameof(IInvokable.Invoke));

            invokeModel = new CallerModel(invokeMethod.Name);
            return true;
        }

        public bool TryBuildDispatchModel(PropertyInfo dispatchProperty, out IDispatchModel dispatchModel)
        {
            if (!dispatchProperty.Name.Equals(nameof(IFunctionModel.Dispatch), StringComparison.OrdinalIgnoreCase))
            {
                dispatchModel = null;
                return false;
            }

            EnsureMemberType(dispatchProperty.PropertyType != typeof(Action), dispatchProperty.Name, nameof(Action), nameof(IFunctionModel.Dispatch));

            dispatchModel = new CallerModel(dispatchProperty.Name);
            return true;
        }

        public bool TryBuildInputModel(PropertyInfo inputProperty, string functionFullName, out IInputModel inputModel)
        {
            var attribute = inputProperty.GetCustomAttribute<InputAttribute>();

            if (attribute == null)
            {
                inputModel = null;
                return false;
            }

            EnsureMemberType(inputProperty.PropertyType.Name != "Func`1" || inputProperty.PropertyType.GenericTypeArguments.Length != 1, inputProperty.Name, "Func with one parameter", nameof(IFunctionModel.Inputs));

            InputType inputType = InputType.Plain;
            IInputValueModel defaultValue = null;
            IEnumerable<IInputValueModel> valueOptions = null;

            if (attribute.ValueFactory != null)
            {
                inputType = InputType.Field;

                _inputValueService.StoreValueFactory(functionFullName, inputProperty.Name, attribute.ValueFactory);

                bool hasDefaultValue = false;

                if (attribute.DefaultValueParams != null)
                    hasDefaultValue = _inputValueService.TryGetDefaultValue(attribute.ValueFactory, out defaultValue, attribute.DefaultValueParams);

                bool hasValueOptions = false;

                if (attribute.ValueOptionsParams != null)
                    hasValueOptions = _inputValueService.TryGetValueOptions(attribute.ValueFactory, out valueOptions, attribute.ValueOptionsParams);

                if (hasValueOptions)
                {
                    inputType = InputType.Dropdown;
                }
                else if (inputProperty.PropertyType == typeof(bool))
                {
                    inputType = InputType.Checkbox;
                }
            }

            inputModel = new InputModel(
                name: inputProperty.Name,
                typeName: inputProperty.PropertyType.GenericTypeArguments[0].Name,
                displayName: _displayConverter.GetDisplayName(attribute.DisplayName, functionFullName + inputProperty.Name, inputProperty.Name),
                description: _displayConverter.GetDescription(attribute.Description),
                displayColor: _displayConverter.GetDisplayColor(inputProperty.PropertyType.GenericTypeArguments[0].Name),
                inputType: inputType,
                defaultValue: defaultValue,
                valueOptions: valueOptions
            );
            return true;
        }

        public bool TryBuildOutputModel(PropertyInfo outputProperty, string functionFullName, out IOutputModel outputModel)
        {
            var attribute = outputProperty.GetCustomAttribute<OutputAttribute>();

            if (attribute == null)
            {
                outputModel = null;
                return false;
            }

            EnsureMemberType(outputProperty.PropertyType.GetTypeInfo().IsGenericType, outputProperty.Name, outputProperty.PropertyType.Name, nameof(IFunctionModel.Outputs));

            outputModel = new OutputModel(
                name: outputProperty.Name,
                typeName: outputProperty.PropertyType.Name,
                displayName: _displayConverter.GetDisplayName(attribute.DisplayName, functionFullName + outputProperty.Name, outputProperty.Name),
                description: _displayConverter.GetDescription(attribute.Description),
                displayColor: _displayConverter.GetDisplayColor(outputProperty.PropertyType.Name)
            );
            return true;
        }

        private void EnsureMemberType(bool invalidCondition, string functionMemberName, string typeName, string modelMemberName)
        {
            if (invalidCondition)
                throw new InvalidReturnTypeException($"The return type of {functionMemberName} must be {typeName} in order to be used for {modelMemberName}");
        }
    }
}