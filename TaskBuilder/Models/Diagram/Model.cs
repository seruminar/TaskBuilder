using System.Collections.Generic;

namespace TaskBuilder.Models.Diagram
{
    public class Model
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string DisplayColor { get; set; }

        public Invoke Invoke { get; set; }

        public Dispatch Dispatch { get; set; }

        public ICollection<Input> Inputs { get; set; }

        public ICollection<Output> Outputs { get; set; }
    }
}