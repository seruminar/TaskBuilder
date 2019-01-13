using TaskBuilder.Attributes;

namespace TaskBuilder.Models.Function
{
    internal class InputModel : IInputModel
    {
        public string Name { get; }

        public string TypeName { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public InputType InputType { get; }

        public object DefaultValue { get; }

        public InputOptionModel[] InputOptions { get; }

        public InputModel(string name, string fullName, string typeName, InputAttribute attribute)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = TaskBuilderHelper.GetDisplayName(attribute.DisplayName, fullName, name);
            DisplayColor = TaskBuilderHelper.GetDisplayColor(typeName);
            InputType = attribute.InputType;
            DefaultValue = attribute.DefaultValue;
            InputOptions = attribute.InputOptions;
        }
    }
}