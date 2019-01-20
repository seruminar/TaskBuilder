using System;
using System.Reflection;

using TaskBuilder.Attributes;
using TaskBuilder.Functions;
using TaskBuilder.Models;
using TaskBuilder.Models.Function;
using TaskBuilder.Models.Function.Exceptions;
using TaskBuilder.Services.Inputs;

namespace TaskBuilder.Services.Functions
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

        public FunctionModel BuildSimpleFunctionModel(string typeName, string assemblyName)
        {
            return new FunctionModel(typeName, assemblyName);
        }

        public FunctionModel BuildFunctionModel(Type functionType)
        {
            var attribute = functionType.GetCustomAttribute<FunctionAttribute>();

            var functionModel = new FunctionModel(
                typeName: functionType.FullName,
                assembly: functionType.Assembly.GetName().Name,
                displayName: _displayConverter.DisplayNameFrom(attribute.DisplayName, functionType.FullName, functionType.Name),
                displayColor: _displayConverter.DisplayColorFrom(null, attribute.DisplayColor)
                );

            CallerModel invokeModel;

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

            foreach (var property in functionType.GetProperties())
            {
                CallerModel dispatchModel;

                if (TryBuildDispatchModel(property, out dispatchModel))
                {
                    functionModel.Dispatchs.Add(dispatchModel);
                    continue;
                }

                InputModel inputModel;

                if (TryBuildInputModel(property, functionType.FullName, out inputModel))
                {
                    functionModel.Inputs.Add(inputModel);
                    continue;
                }

                OutputModel outputModel;

                if (TryBuildOutputModel(property, functionType.FullName, out outputModel))
                {
                    functionModel.Outputs.Add(outputModel);
                    continue;
                }
            }

            return functionModel;
        }

        public bool TryBuildInvokeModel(MethodInfo invokeMethod, out CallerModel invokeModel)
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

        public bool TryBuildDispatchModel(PropertyInfo dispatchProperty, out CallerModel dispatchModel)
        {
            if (!(dispatchProperty.Name.Equals(nameof(IDispatcher.Dispatch), StringComparison.OrdinalIgnoreCase)
                || dispatchProperty.Name.Equals(nameof(IDispatcher2.Dispatch2), StringComparison.OrdinalIgnoreCase)))
            {
                dispatchModel = null;
                return false;
            }

            EnsureMemberType(dispatchProperty.PropertyType != typeof(Action), dispatchProperty.Name, nameof(Action), "Dispatchs");

            dispatchModel = new CallerModel(dispatchProperty.Name);
            return true;
        }

        public bool TryBuildInputModel(PropertyInfo inputProperty, string functionFullName, out InputModel inputModel)
        {
            var attribute = inputProperty.GetCustomAttribute<InputAttribute>();

            if (attribute == null)
            {
                inputModel = null;
                return false;
            }

            EnsureMemberType(inputProperty.PropertyType.Name != "Func`1" || inputProperty.PropertyType.GenericTypeArguments.Length != 1, inputProperty.Name, "Func with one parameter", "Inputs");

            InputType inputType = InputType.Plain;
            InputFieldsModel fieldsModel = null;
            InputFieldsModel defaultFieldsModel = null;

            if (attribute.ValueBuilder != null)
            {
                bool hasEmptyValue = false;
                bool hasValueOptions = false;
                bool hasDefaultValue = false;

                if (attribute.ValueOptionsParams == null && attribute.ValueParams == null)
                {
                    hasEmptyValue = _inputValueService.TryGetEmptyFields(attribute.ValueBuilder, out fieldsModel);
                }
                else if (attribute.ValueOptionsParams != null)
                {
                    hasValueOptions = _inputValueService.TryGetOptions(attribute.ValueBuilder, out fieldsModel, attribute.ValueOptionsParams);
                    hasDefaultValue = _inputValueService.TryGetDefaultFields(attribute.ValueBuilder, out defaultFieldsModel, attribute.ValueParams);
                }
                else if (attribute.ValueParams != null)
                {
                    hasEmptyValue = _inputValueService.TryGetEmptyFields(attribute.ValueBuilder, out fieldsModel);
                    hasDefaultValue = _inputValueService.TryGetDefaultFields(attribute.ValueBuilder, out defaultFieldsModel, attribute.ValueParams);
                }

                if (hasEmptyValue)
                {
                    inputType = InputType.Fields;
                }
                if (hasValueOptions)
                {
                    inputType = InputType.Dropdown;
                }

                _inputValueService.StoreValueBuilder(functionFullName, inputProperty.Name, attribute.ValueBuilder);
            }

            inputModel = new InputModel(
                name: inputProperty.Name,
                typeName: inputProperty.PropertyType.GenericTypeArguments[0].Name,
                displayName: _displayConverter.DisplayNameFrom(attribute.DisplayName, functionFullName + inputProperty.Name, inputProperty.Name),
                description: _displayConverter.DescriptionFrom(attribute.Description),
                displayColor: _displayConverter.DisplayColorFrom(inputProperty.PropertyType.GenericTypeArguments[0].Name)
                )
            {
                InputType = inputType,
                FieldsModel = fieldsModel,
                DefaultFieldsModel = defaultFieldsModel
            };

            return true;
        }

        public bool TryBuildOutputModel(PropertyInfo outputProperty, string functionFullName, out OutputModel outputModel)
        {
            var attribute = outputProperty.GetCustomAttribute<OutputAttribute>();

            if (attribute == null)
            {
                outputModel = null;
                return false;
            }

            EnsureMemberType(outputProperty.PropertyType.GetTypeInfo().IsGenericType, outputProperty.Name, outputProperty.PropertyType.Name, "Outputs");

            outputModel = new OutputModel(
                name: outputProperty.Name,
                typeName: outputProperty.PropertyType.Name,
                displayName: _displayConverter.DisplayNameFrom(attribute.DisplayName, functionFullName + outputProperty.Name, outputProperty.Name),
                description: _displayConverter.DescriptionFrom(attribute.Description),
                displayColor: _displayConverter.DisplayColorFrom(outputProperty.PropertyType.Name)
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