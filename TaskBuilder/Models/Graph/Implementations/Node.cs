using System;
using System.Collections.Generic;
using System.Diagnostics;

using Newtonsoft.Json;

namespace TaskBuilder.Models.Graph
{
    [DebuggerDisplay("{Type}: {FunctionTypeGuid}")]
    public class Node : INode
    {
        public Guid Id { get; set; }

        public bool Selected { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public IEnumerable<IPort> Ports { get; set; }

        public NodeType Type { get; set; }

        public Guid FunctionTypeGuid { get; set; }

        [JsonConstructor]
        public Node(Guid id, bool selected, double x, double y, IEnumerable<Port> ports, NodeType type, Guid functionTypeGuid)
        {
            Id = id;
            Selected = selected;
            X = x;
            Y = y;
            Ports = ports;
            Type = type;
            FunctionTypeGuid = functionTypeGuid;
        }
    }
}