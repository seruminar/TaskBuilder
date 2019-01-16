using System.Collections.Generic;

namespace TaskBuilder.Models.Diagram
{
    public class Input
    {
        public string Name { get; set; }

        public string TypeName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string DisplayColor { get; set; }

        public InputType InputType { get; set; }

        public InputValue DefaultValue { get; set; }

        public IEnumerable<InputValue> ValueOptions { get; set; }
    }
}