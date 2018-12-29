using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using TaskBuilder.Attributes;

namespace TaskBuilder.Models
{
    public class FunctionModel : BaseFunctionModel
    {
        [JsonProperty("enter")]
        public string Enter { get; }

        [JsonProperty("enterDisplayName")]
        public string EnterDisplayName { get; } = string.Empty;

        [JsonProperty("leave")]
        public string Leave { get; }

        [JsonProperty("leaveDisplayName")]
        public string LeaveDisplayName { get; } = string.Empty;

        [JsonProperty("inputs")]
        public ICollection<string> Inputs { get; } = new List<string>();

        [JsonProperty("inputsDisplayNames")]
        public ICollection<string> InputsDisplayNames { get; } = new List<string>();

        [JsonProperty("outputs")]
        public ICollection<string> Outputs { get; } = new List<string>();

        [JsonProperty("outputsDisplayNames")]
        public ICollection<string> OutputsDisplayNames { get; } = new List<string>();

        public FunctionModel(Type function) : base(function)
        {
            foreach (var method in function.GetMethods())
            {
                var attribute = method.GetCustomAttribute<EnterAttribute>();

                if (attribute == null)
                    continue;

                Enter = method.Name;
                EnterDisplayName = attribute.DisplayName;
                break;
            }

            foreach (var property in function.GetProperties())
            {
                BaseFunctionAttribute attribute = property.GetCustomAttribute<LeaveAttribute>();

                if (attribute != null)
                {
                    Leave = property.Name;
                    LeaveDisplayName = attribute.DisplayName;
                    continue;
                }

                attribute = property.GetCustomAttribute<InputAttribute>();

                if (attribute != null)
                {
                    Inputs.Add(property.Name);
                    InputsDisplayNames.Add(attribute.DisplayName);
                    continue;
                }

                attribute = property.GetCustomAttribute<OutputAttribute>();

                if (attribute != null)
                {
                    Outputs.Add(property.Name);
                    OutputsDisplayNames.Add(attribute.DisplayName);
                }
            }
        }
    }
}