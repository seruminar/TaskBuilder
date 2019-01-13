using TaskBuilder.Attributes;

namespace TaskBuilder.Models.Function
{
    internal class OutputModel : IOutputModel
    {
        public string Name { get; }

        public string TypeName { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public OutputModel(string name, string fullName, string typeName, OutputAttribute attribute)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = TaskBuilderHelper.GetDisplayName(attribute.DisplayName, fullName, name);

            DisplayColor = TaskBuilderHelper.GetDisplayColor(typeName);
        }
    }
}