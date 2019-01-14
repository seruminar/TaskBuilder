using System.Collections.Generic;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.Diagram
{
    public class Input : IInputModel
    {
        public InputType InputType { get; set; }

        public string Value { get; set; }

        public InputValueModel DefaultValue { get; set; }

        public IEnumerable<InputValueModel> ValueOptions { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public string DisplayName { get; set; }

        public string DisplayColor { get; set; }
    }
}