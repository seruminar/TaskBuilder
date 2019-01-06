namespace TaskBuilder.Models
{
    public interface ITypedModel : IBaseModel
    {
        string DisplayType { get; }
    }
}