using System;
using System.Collections.Generic;
using System.Diagnostics;

using TaskBuilder.Functions;

namespace TaskBuilder.Models.Graph
{
    [DebuggerDisplay("{Type}: {Color}")]
    public class Link : ILink
    {
        public Guid Id { get; set; }

        public bool Selected { get; set; }

        public Guid Source { get; set; }

        public Guid SourcePort { get; set; }

        public Guid Target { get; set; }

        public Guid TargetPort { get; set; }

        public ICollection<Point> Points { get; set; }

        public ICollection<object> Labels { get; set; }

        public LinkType Type { get; set; }

        public string Color { get; set; }
    }
}