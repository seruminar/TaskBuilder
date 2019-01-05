using System;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using TaskBuilder.Attributes;

namespace TaskBuilder.Models
{
    [DebuggerDisplay("{DisplayName}: {Name}")]
    public class TypedModel : ITypedModel
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("displayName")]
        public string DisplayName { get; protected set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; protected set; } = string.Empty;

        [JsonProperty("displayType")]
        public string DisplayType { get; protected set; }

        /// <summary>
        /// Use this constructor to create a model for a <see cref="FunctionModel"/>.
        /// </summary>
        /// <param name="function"></param>
        internal TypedModel(Type function)
        {
            var attribute = function.GetCustomAttribute<FunctionAttribute>();

            if (attribute != null)
            {
                Name = function.FullName;
                DisplayName = attribute.DisplayName;
            }
        }

        public TypedModel(string name)
        {
            Name = name;
        }
    }
}