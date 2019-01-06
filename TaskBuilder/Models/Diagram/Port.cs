using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TaskBuilder.Models.Diagram
{
    public class Port : ColoredModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("parentNode")]
        public Guid ParentNode { get; set; }

        [JsonProperty("links")]
        public ICollection<Guid> Links { get; set; }
    }
}