namespace TaskBuilder.Models.Function
{
    public sealed class InputValueModel : IInputValueModel
    {
        public string Value { get; }

        public object[] ValueParams { get; }

        public InputValueModel(string displayName = null, object[] valueParams = null)
        {
            Value = displayName;
            ValueParams = valueParams;
        }
    }
}