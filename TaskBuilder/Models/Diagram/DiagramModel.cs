using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TaskBuilder.Models
{
    public class DiagramModel
    {
        public DiagramModel(Guid taskGuid)
        {
            Id = taskGuid;
            Zoom = 100;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("offsetX")]
        public long OffsetX { get; set; }

        [JsonProperty("offsetY")]
        public long OffsetY { get; set; }

        [JsonProperty("zoom")]
        public long Zoom { get; set; }

        [JsonProperty("gridSize")]
        public long GridSize { get; set; }

        [JsonProperty("links")]
        public ICollection<Link> Links { get; set; }

        [JsonProperty("nodes")]
        public ICollection<Node> Nodes { get; set; }
    }
}