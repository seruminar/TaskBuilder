using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskBuilder.Models.Diagram
{
    public class Diagram
    {
        public Diagram(Guid taskGuid)
        {
            Id = taskGuid;
            Zoom = 100;
        }

        public Guid Id { get; set; }

        public long OffsetX { get; set; }

        public long OffsetY { get; set; }

        public long Zoom { get; set; }

        public long GridSize { get; set; }

        public ICollection<Link> Links { get; set; }

        public ICollection<Node> Nodes { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, TaskBuilderHelper.JsonSerializerSettings);
        }
    }
}