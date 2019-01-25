using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Diagram
{
    public class Link
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public bool Selected { get; set; }

        public Guid Source { get; set; }

        public Guid SourcePort { get; set; }

        public Guid Target { get; set; }

        public Guid TargetPort { get; set; }

        public ICollection<Point> Points { get; set; }

        public Extras Extras { get; set; }

        public ICollection<object> Labels { get; set; }

        public string Color { get; set; }
    }
}