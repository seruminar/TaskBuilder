using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.Diagram
{
    public class Input : IInputModel
    {
        public InputType InputType { get; set; }

        public object DefaultValue { get; set; }

        public InputOptionModel[] InputOptions { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public string DisplayName { get; set; }

        public string DisplayColor { get; set; }
    }
}