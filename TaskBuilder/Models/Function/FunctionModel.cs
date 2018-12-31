using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using TaskBuilder.Attributes;

namespace TaskBuilder.Models
{
    public class FunctionModel : TypedModel
    {
        [JsonProperty("enter")]
        public PortModel Enter { get; }

        [JsonProperty("leave")]
        public PortModel Leave { get; }

        [JsonProperty("inputs")]
        public ICollection<PortModel> Inputs { get; } = new List<PortModel>();

        [JsonProperty("outputs")]
        public ICollection<PortModel> Outputs { get; } = new List<PortModel>();

        public FunctionModel(Type function) : base(function)
        {
            foreach (var method in function.GetMethods())
            {
                var attribute = method.GetCustomAttribute<EnterAttribute>();

                if (attribute == null)
                    continue;

                Enter = new PortModel(method.Name)
                {
                    DisplayName = attribute.DisplayName,
                    Type = method.ReturnType.FullName
                };
                break;
            }

            foreach (var property in function.GetProperties())
            {
                BaseFunctionAttribute attribute = property.GetCustomAttribute<LeaveAttribute>();

                if (attribute != null)
                {
                    Leave = new PortModel(property.Name)
                    {
                        DisplayName = attribute.DisplayName,
                        Type = property.PropertyType.FullName
                    };
                    continue;
                }

                attribute = property.GetCustomAttribute<InputAttribute>();

                if (attribute != null)
                {
                    Inputs.Add(new PortModel(property.Name)
                    {
                        DisplayName = attribute.DisplayName,
                        Type = property.PropertyType.FullName
                    });
                    continue;
                }

                attribute = property.GetCustomAttribute<OutputAttribute>();

                if (attribute != null)
                {
                    Outputs.Add(new PortModel(property.Name)
                    {
                        DisplayName = attribute.DisplayName,
                        Type = property.PropertyType.FullName
                    });
                }
            }

            if (Enter == null)
            {
                throw new MissingAttributeException($"The type {nameof(function)} does not have a method decorated with {nameof(EnterAttribute)}. This is required for the function to be called.");
            }
        }
    }
}