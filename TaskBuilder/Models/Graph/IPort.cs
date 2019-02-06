using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Graph
{
    public interface IPort : ISelectableModel
    {
        ICollection<Guid> Links { get; set; }

        PortType Type { get; set; }

        string Name { get; set; }
    }
}