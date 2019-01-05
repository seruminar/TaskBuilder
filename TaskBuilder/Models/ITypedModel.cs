namespace TaskBuilder.Models
{
    public interface ITypedModel
    {
        string Name { get; }

        string DisplayName { get; }

        string Type { get; }

        string DisplayType { get; }
    }
}