using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using TaskBuilder.Attributes;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : TypedModel
    {
        [JsonProperty("enter")]
        public ITypedModel Enter { get; }

        [JsonProperty("leave")]
        public ITypedModel Leave { get; }

        [JsonProperty("inputs")]
        public ICollection<ITypedModel> Inputs { get; } = new List<ITypedModel>();

        [JsonProperty("outputs")]
        public ICollection<ITypedModel> Outputs { get; } = new List<ITypedModel>();

        public FunctionModel(Type function) : base(function)
        {
            foreach (var method in function.GetMethods())
            {
                MemberModel enter;

                if (TryBuildEnter(method, out enter))
                {
                    Enter = enter;
                    break;
                }

                continue;
            }

            if (Enter == null)
            {
                throw new MissingAttributeException($"The type {nameof(function)} does not have a method decorated with {nameof(EnterAttribute)}. This is required for the function to be called.");
            }

            foreach (var property in function.GetProperties())
            {
                MemberModel model;

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

        private bool TryBuildEnter(MethodInfo method, out MemberModel enter)
        {
            var attribute = method.GetCustomAttribute<EnterAttribute>();

            if (attribute == null)
            {
                enter = null;
                return false;
            }

            if (method.ReturnType != typeof(void))
                throw new InvalidReturnTypeException($"The return type of {method.Name} must be void in order to be used as {nameof(Enter)}");

            enter = new MemberModel(method.Name,
                attribute.DisplayName,
                method.ReturnType.Name,
                MemberModelTypeEnum.Enter.ToString().ToLower()
            );
            return true;
        }

        private bool TryBuildLeave(PropertyInfo property, out MemberModel leave)
        {
            var attribute = property.GetCustomAttribute<LeaveAttribute>();

            if (attribute == null)
            {
                leave = null;
                return false;
            }

            if (property.PropertyType != typeof(Action))
                throw new InvalidReturnTypeException($"The return type of {property.Name} must be {nameof(Action)} in order to be used as {nameof(Leave)}.");

            leave = new MemberModel(property.Name,
                attribute.DisplayName,
                property.PropertyType.Name,
                MemberModelTypeEnum.Leave.ToString().ToLower()
            );
            return true;
        }

        private bool TryAddInput(PropertyInfo property, out MemberModel input)
        {
            var attribute = property.GetCustomAttribute<InputAttribute>();

            if (attribute == null)
            {
                input = null;
                return false;
            }

            if (property.PropertyType.Name != "Func`1" || property.PropertyType.GenericTypeArguments.Length != 1)
                throw new InvalidReturnTypeException($"The return type of {property.Name} must be a Func with one parameter in order to be used for {nameof(Inputs)}.");

            input = new MemberModel(property.Name,
                attribute.DisplayName,
                property.PropertyType.GenericTypeArguments[0].Name,
                MemberModelTypeEnum.Input.ToString().ToLower()
            );
            return true;
        }

        private bool TryAddOutput(PropertyInfo property, out MemberModel output)
        {
            var attribute = property.GetCustomAttribute<OutputAttribute>();

            if (attribute == null)
            {
                output = null;
                return false;
            }

            output = new MemberModel(property.Name,
                attribute.DisplayName,
                property.PropertyType.Name,
                MemberModelTypeEnum.Output.ToString().ToLower()
            );
            return true;
        }
    }
}