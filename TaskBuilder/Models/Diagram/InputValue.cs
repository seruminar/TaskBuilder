using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.Diagram
{
    public class InputValue : IInputValueModel
    {
        public string Value { get; set; }

        public object[] ValueParams { get; set; }
    }
}