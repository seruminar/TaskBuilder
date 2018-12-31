using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TaskBuilder.Models
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
        public Guid Name { get; set; }

        [JsonProperty("parentNode")]
        public Guid ParentNode { get; set; }

        [JsonProperty("links")]
        public ICollection<Guid> Links { get; set; }

        [JsonProperty("in")]
        public bool In { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}