namespace TaskBuilder.Models.Function
{
    public class OutputModel : IOutputModel
    {
        public string Name { get; }

        public string TypeName { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public string DisplayColor { get; }

        public OutputModel(string name, string typeName, string displayName, string description, string displayColor)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
        }
    }
}