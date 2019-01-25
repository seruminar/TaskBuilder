namespace TaskBuilder.Models.Function
{
    public class DispatchModel : IDispatchModel
    {
        public string Name { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public DispatchModel(string name, string displayName, string description)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
        }
    }
}