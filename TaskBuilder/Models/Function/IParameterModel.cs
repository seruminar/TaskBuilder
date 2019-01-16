namespace TaskBuilder.Models.Function
{
    public interface IParameterModel : IPortModel
    {
        string TypeName { get; }

        string DisplayName { get; }

        string Description { get; }

        string DisplayColor { get; }
    }
}