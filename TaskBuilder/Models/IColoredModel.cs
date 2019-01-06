namespace TaskBuilder.Models
{
    public interface IColoredModel : ITypedModel
    {
        string DisplayColor { get; }
    }
}