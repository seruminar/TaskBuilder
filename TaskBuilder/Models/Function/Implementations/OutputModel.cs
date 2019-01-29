using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class OutputModel : IOutputModel
    {
        public string Name { get; }

        public ICollection<string> TypeNames { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public string DisplayColor { get; }

        public OutputModel(string name, ICollection<string> typeNames, string displayName, string description, string displayColor)
        {
            Name = name;
            TypeNames = typeNames;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
        }
    }
}