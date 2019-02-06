using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Graph
{
    public interface ILink : ISelectableModel
    {
        Guid Source { get; set; }

        Guid SourcePort { get; set; }

        Guid Target { get; set; }

        Guid TargetPort { get; set; }

        ICollection<Point> Points { get; set; }

        ICollection<object> Labels { get; set; }

        LinkType Type { get; set; }

        string Color { get; set; }
    }
}