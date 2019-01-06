namespace TaskBuilder.Models
{
    public interface IBaseModel
    {
        string DisplayName { get; }

        string Type { get; }
    }
}