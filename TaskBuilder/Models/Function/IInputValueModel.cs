namespace TaskBuilder.Models.Function
{
    public interface IInputValueModel
    {
        string Value { get; }

        object[] ValueParams { get; }
    }
}