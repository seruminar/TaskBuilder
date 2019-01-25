namespace TaskBuilder.Models.Function
{
    public interface IParameterModel : INamedPortModel
    {
        string TypeName { get; }

        string DisplayColor { get; }
    }
}