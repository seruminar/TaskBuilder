using System;
using System.Reflection;

using TaskBuilder.Attributes;
using TaskBuilder.Functions;
using TaskBuilder.Models.Function;
using TaskBuilder.Models.Function.InputValue;
using TaskBuilder.Services.Functions.Exceptions;
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

        public IFunctionModel BuildFunctionModel(string functionTypeIdentifier, Type functionType)
        {
            var attribute = functionType.GetCustomAttribute<FunctionAttribute>();

            var functionModel = new FunctionModel(
                typeIdentifier: functionTypeIdentifier,
                displayName: _displayConverter.DisplayNameFrom(attribute.DisplayName, functionType.FullName, functionType.Name),
                displayColor: _displayConverter.DisplayColorFrom(null, attribute.DisplayColor)
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

            foreach (var property in functionType.GetProperties())
            {
                IDispatchModel dispatchModel;

                if (TryBuildDispatchModel(property, out dispatchModel))
                {
                    functionModel.Dispatchs.Add(dispatchModel);
                    continue;
                }

                IInputModel inputModel;

                if (TryBuildInputModel(property, functionType.FullName, functionTypeIdentifier, out inputModel))
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

        public bool TryBuildInputModel(PropertyInfo inputProperty, string functionFullName, string functionTypeIdentifier, out IInputModel inputModel)
        {
            var attribute = inputProperty.GetCustomAttribute<InputAttribute>();

            if (attribute == null)
            {
                inputModel = null;
                return false;
            }

            EnsureMemberType(inputProperty.PropertyType.Name != "Func`1" || inputProperty.PropertyType.GenericTypeArguments.Length != 1, inputProperty.Name, "Func with one parameter", "Inputs");

            InputType inputType = InputType.Plain;
            IInputValueModel structureModel = null;
            IInputValueModel filledModel = null;

            if (attribute.ValueBuilder != null)
            {
                _inputValueService.TryGetStructureModel(attribute.ValueBuilder, out structureModel, attribute.StructureModelParams);
                inputType = InputType.StructureOnly;

                if (attribute.FilledModelParams != null)
                {
                    _inputValueService.TryGetFilledModel(attribute.ValueBuilder, structureModel, out filledModel, attribute.FilledModelParams);
                    inputType = InputType.Filled;
                }

                _inputValueService.StoreValueBuilder(functionTypeIdentifier, inputProperty.Name, attribute.ValueBuilder);
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
                StructureModel = structureModel,
                FilledModel = filledModel
            };

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