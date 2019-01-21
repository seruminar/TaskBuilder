using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Diagram
{
    public class Port
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public bool Selected { get; set; }

        public string Name { get; set; }

        public Guid ParentNode { get; set; }

        public ICollection<Guid> Links { get; set; }

        public bool Linked { get; set; }
    }
}