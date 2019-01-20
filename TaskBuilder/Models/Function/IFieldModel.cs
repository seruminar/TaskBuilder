namespace TaskBuilder.Models.Function
{
    public interface IFieldModel
    {
        string Key { get; }

        FieldParameter Value { get; }
    }
}