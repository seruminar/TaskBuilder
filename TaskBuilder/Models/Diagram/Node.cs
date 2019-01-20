using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.Diagram
{
    [DebuggerDisplay("{Type}")]
    public class Node
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public bool Selected { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public Extras Extras { get; set; }

        public ICollection<Port> Ports { get; set; }

        public FunctionModel Function { get; set; }
    }
}