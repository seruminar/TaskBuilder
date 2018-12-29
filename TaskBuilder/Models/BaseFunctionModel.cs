using System;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using TaskBuilder.Attributes;

namespace TaskBuilder.Models
{
    [DebuggerDisplay("{DisplayName}: {Name}")]
    public class BaseFunctionModel : IFunctionModel
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("displayName")]
        public string DisplayName { get; } = string.Empty;

        internal BaseFunctionModel(Type function)
        {
            var attribute = function.GetCustomAttribute<FunctionAttribute>();

            if (attribute != null)
            {
                Name = function.FullName;
                DisplayName = attribute.DisplayName;
            }
        }

        public BaseFunctionModel(string name)
        {
            Name = name;
        }
    }
}