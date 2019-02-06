using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Graph
{
    public interface INode : ISelectableModel
    {
        double X { get; set; }

        double Y { get; set; }

        IEnumerable<IPort> Ports { get; set; }

        NodeType Type { get; set; }

        Guid FunctionTypeGuid { get; set; }
    }
}