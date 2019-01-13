using System;
using System.Collections.Generic;
using System.Reflection;

using TaskBuilder.Attributes;
using TaskBuilder.Models.Function.Exceptions;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : IFunctionModel
    {
        private readonly string INVOKE = "Invoke";
        private readonly string DISPATCH = "Dispatch";

        public string Name { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public IInvokeModel Invoke { get; }

        public IDispatchModel Dispatch { get; }

        public ICollection<IInputModel> Inputs { get; } = new List<IInputModel>();

        public ICollection<IOutputModel> Outputs { get; } = new List<IOutputModel>();

        internal FunctionModel(string name)
        {
            Name = name;
        }

        public FunctionModel(Type functionType)
        {
            Name = functionType.FullName;

            var attribute = functionType.GetCustomAttribute<FunctionAttribute>();

            DisplayName = TaskBuilderHelper.GetDisplayName(attribute.DisplayName, functionType.FullName, functionType.Name);
            DisplayColor = TaskBuilderHelper.GetDisplayColor(null, attribute.DisplayColor);

            foreach (var method in functionType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                CallerModel invoke;

                if (TryBuildInvoke(method, out invoke))
                {
                    Invoke = invoke;
                    break;
                }

                continue;
            }

            if (Invoke == null)
                throw new MissingMethodException($"The type {nameof(functionType)} does not have a method named {INVOKE}. This is required for the function to be invoked.");

            CallerModel dispatch;

            foreach (var property in functionType.GetProperties())
            {
                if (Dispatch == null && TryBuildDispatch(property, out dispatch))
                {
                    Dispatch = dispatch;
                    continue;
                }

                InputModel input;

                if (TryAddInput(property, functionType.FullName, out input))
                {
                    Inputs.Add(input);
                    continue;
                }

                OutputModel output;

                if (TryAddOutput(property, functionType.FullName, out output))
                {
                    Outputs.Add(output);
                    continue;
                }
            }
        }

        private bool TryBuildInvoke(MethodInfo method, out CallerModel invoke)
        {
            if (!method.Name.Equals(INVOKE, StringComparison.OrdinalIgnoreCase))
            {
                invoke = null;
                return false;
            }

            EnsureMemberType(method.ReturnType != typeof(void), method.Name, "void", nameof(Invoke));

            invoke = new CallerModel(method.Name);
            return true;
        }

        private bool TryBuildDispatch(PropertyInfo property, out CallerModel dispatch)
        {
            if (!property.Name.Equals(DISPATCH, StringComparison.OrdinalIgnoreCase))
            {
                dispatch = null;
                return false;
            }

            EnsureMemberType(property.PropertyType != typeof(Action), property.Name, nameof(Action), nameof(Dispatch));

            dispatch = new CallerModel(property.Name);
            return true;
        }

        private bool TryAddInput(PropertyInfo property, string functionFullName, out InputModel input)
        {
            var attribute = property.GetCustomAttribute<InputAttribute>();

            if (attribute == null)
            {
                input = null;
                return false;
            }

            EnsureMemberType(property.PropertyType.Name != "Func`1" || property.PropertyType.GenericTypeArguments.Length != 1, property.Name, "Func with one parameter", nameof(Inputs));

            input = new InputModel(property.Name,
                functionFullName + property.Name,
                property.PropertyType.GenericTypeArguments[0].Name,
                attribute);
            return true;
        }

        private bool TryAddOutput(PropertyInfo property, string functionFullName, out OutputModel output)
        {
            var attribute = property.GetCustomAttribute<OutputAttribute>();

            if (attribute == null)
            {
                output = null;
                return false;
            }

            EnsureMemberType(property.PropertyType.GetTypeInfo().IsGenericType, property.Name, property.PropertyType.Name, nameof(Outputs));

            output = new OutputModel(property.Name,
                functionFullName + property.Name,
                property.PropertyType.Name,
                attribute
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