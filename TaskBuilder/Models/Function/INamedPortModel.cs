namespace TaskBuilder.Models.Function
{
    public interface INamedPortModel : IPortModel
    {
        string DisplayName { get; }

        string Description { get; }
    }
}