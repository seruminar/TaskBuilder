using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TaskBuilder.Models.Diagram
{
    public class Node
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("extras")]
        public Extras Extras { get; set; }

        [JsonProperty("ports")]
        public ICollection<Port> Ports { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}