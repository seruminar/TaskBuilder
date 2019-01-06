using System;
using System.Collections.Generic;
using System.Reflection;

using Newtonsoft.Json;

using TaskBuilder.Attributes;
using TaskBuilder.Models.Diagram;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : BaseModel, IFunctionModel
    {
        [JsonProperty("enter")]
        public ITypedModel Enter { get; }

        [JsonProperty("leave")]
        public ITypedModel Leave { get; }

        [JsonProperty("inputs")]
        public ICollection<IColoredModel> Inputs { get; } = new List<IColoredModel>();

        [JsonProperty("outputs")]
        public ICollection<IColoredModel> Outputs { get; } = new List<IColoredModel>();

        internal FunctionModel(string type)
        {
            Type = type;
        }

        public FunctionModel(Type functionType)
        {
            var attribute = functionType.GetCustomAttribute<FunctionAttribute>();

            if (attribute != null)
            {
                Type = functionType.FullName;
                DisplayName = attribute.DisplayName ?? functionType.FullName;
            }

            foreach (var method in functionType.GetMethods())
            {
                PortModel enter;

                if (TryBuildEnter(method, out enter))
                {
                    Enter = enter;
                    break;
                }

                continue;
            }

            if (Enter == null)
            {
                throw new MissingAttributeException($"The type {nameof(functionType)} does not have a method decorated with {nameof(EnterAttribute)}. This is required for the function to be called.");
            }

            foreach (var property in functionType.GetProperties())
            {
                PortModel model;

                if (Leave == null && TryBuildLeave(property, out model))
                {
                    Leave = model;
                }
                else
                if (TryAddInput(property, out model))
                {
                    Inputs.Add(model);
                }
                else
                if (TryAddOutput(property, out model))
                {
                    Outputs.Add(model);
                }
            }
        }

        private bool TryBuildEnter(MethodInfo method, out PortModel enter)
        {
            var attribute = method.GetCustomAttribute<EnterAttribute>();

            if (attribute == null)
            {
                enter = null;
                return false;
            }

            if (method.ReturnType != typeof(void))
                throw new InvalidReturnTypeException($"The return type of {method.Name} must be void in order to be used as {nameof(Enter)}");

            enter = new PortModel(method.Name,
                attribute.DisplayName,
                method.ReturnType.Name,
                PortTypeEnum.Enter,
                attribute.DisplayColor
            );
            return true;
        }

        private bool TryBuildLeave(PropertyInfo property, out PortModel leave)
        {
            var attribute = property.GetCustomAttribute<LeaveAttribute>();

            if (attribute == null)
            {
                leave = null;
                return false;
            }

            if (property.PropertyType != typeof(Action))
                throw new InvalidReturnTypeException($"The return type of {property.Name} must be {nameof(Action)} in order to be used as {nameof(Leave)}.");

            leave = new PortModel(property.Name,
                attribute.DisplayName,
                property.PropertyType.Name,
                PortTypeEnum.Leave,
                attribute.DisplayColor
            );
            return true;
        }

        private bool TryAddInput(PropertyInfo property, out PortModel input)
        {
            var attribute = property.GetCustomAttribute<InputAttribute>();

            if (attribute == null)
            {
                input = null;
                return false;
            }

            if (property.PropertyType.Name != "Func`1" || property.PropertyType.GenericTypeArguments.Length != 1)
                throw new InvalidReturnTypeException($"The return type of {property.Name} must be a Func with one parameter in order to be used for {nameof(Inputs)}.");

            input = new PortModel(property.Name,
                attribute.DisplayName,
                property.PropertyType.GenericTypeArguments[0].Name,
                PortTypeEnum.Input,
                attribute.DisplayColor
            );
            return true;
        }

        private bool TryAddOutput(PropertyInfo property, out PortModel output)
        {
            var attribute = property.GetCustomAttribute<OutputAttribute>();

            if (attribute == null)
            {
                output = null;
                return false;
            }

            output = new PortModel(property.Name,
                attribute.DisplayName,
                property.PropertyType.Name,
                PortTypeEnum.Output,
                attribute.DisplayColor
            );
            return true;
        }
    }
}