using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Graph
{
    public class Port : IPort
    {
        public Guid Id { get; set; }

        public bool Selected { get; set; }

        public ICollection<Guid> Links { get; set; }

        public PortType Type { get; set; }

        public string Name { get; set; }
    }
}