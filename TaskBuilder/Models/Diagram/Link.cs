using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TaskBuilder.Models
{
    public class Link
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("source")]
        public Guid Source { get; set; }

        [JsonProperty("sourcePort")]
        public Guid SourcePort { get; set; }

        [JsonProperty("target")]
        public Guid Target { get; set; }

        [JsonProperty("targetPort")]
        public Guid TargetPort { get; set; }

        [JsonProperty("points")]
        public ICollection<Point> Points { get; set; }

        [JsonProperty("extras")]
        public Extras Extras { get; set; }

        [JsonProperty("labels")]
        public ICollection<object> Labels { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("curvyness")]
        public long Curvyness { get; set; }
    }
}