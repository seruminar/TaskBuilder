using System;
using Newtonsoft.Json;

namespace TaskBuilder
{
    public class BaseFunctionModel : IEquatable<BaseFunctionModel>
    {
        [JsonProperty("name")]
        public string Name { get; }

        public BaseFunctionModel(string name)
        {
            Name = name;
        }

        public bool Equals(BaseFunctionModel other)
        {
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}