using System;
using System.Collections.Generic;

using TaskBuilder.Functions;

namespace TaskBuilder.Models.Diagram
{
    public class Port
    {
        public Guid Id { get; set; }

        public PortType Type { get; set; }

        public bool Selected { get; set; }

        public string Name { get; set; }

        public Guid ParentNode { get; set; }

        public ICollection<Guid> Links { get; set; }
    }
}