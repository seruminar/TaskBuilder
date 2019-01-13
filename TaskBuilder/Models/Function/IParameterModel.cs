namespace TaskBuilder.Models.Function
{
    public interface IParameterModel
    {
        string Name { get; }

        string TypeName { get; }

        string DisplayName { get; }

        string DisplayColor { get; }
    }
}