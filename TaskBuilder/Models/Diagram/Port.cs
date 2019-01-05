using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TaskBuilder.Models.Diagram
{
    public class Port
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentNode")]
        public Guid ParentNode { get; set; }

        [JsonProperty("links")]
        public ICollection<Guid> Links { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("displayType")]
        public string DisplayType { get; set; }
    }
}