namespace TaskBuilder.Models.Function
{
    public sealed class FieldModel : IFieldModel
    {
        public string Key { get; }

        public FieldParameter Value { get; }

        public FieldModel(string key, FieldParameter value)
        {
            Key = key;
            Value = value;
        }
    }
}