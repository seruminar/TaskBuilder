using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using TaskBuilder.Attributes;

namespace TaskBuilder
{
    public class FunctionModel
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("enter")]
        public string Enter { get; }

        [JsonProperty("leave")]
        public string Leave { get; }

        [JsonProperty("inputs")]
        public ICollection<string> Inputs { get; } = new List<string>();

        [JsonProperty("outputs")]
        public ICollection<string> Outputs { get; } = new List<string>();

        public FunctionModel(Type function)
        {
            Name = function.FullName;

            foreach (var method in function.GetMethods())
            {
                if (method.GetCustomAttribute<EnterAttribute>() != null)
                {
                    Enter = method.Name;
                }
            }

            foreach (var property in function.GetProperties())
            {
                if (property.GetCustomAttribute<LeaveAttribute>() != null)
                {
                    Leave = property.Name;
                    continue;
                }

                if (property.GetCustomAttribute<InputAttribute>() != null)
                {
                    Inputs.Add(property.Name);
                    continue;
                }

                if (property.GetCustomAttribute<OutputAttribute>() != null)
                {
                    Outputs.Add(property.Name);
                }
            }
        }
    }
}