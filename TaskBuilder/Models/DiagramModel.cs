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
        public List<Link> Links { get; set; }

        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }
    }

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
        public List<Point> Points { get; set; }

        [JsonProperty("extras")]
        public Extras Extras { get; set; }

        [JsonProperty("labels")]
        public List<object> Labels { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("curvyness")]
        public long Curvyness { get; set; }
    }

    public class Extras
    {
    }

    public class Point
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }

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
        public List<Port> Ports { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

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
        public List<Guid> Links { get; set; }

        [JsonProperty("in")]
        public bool In { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}