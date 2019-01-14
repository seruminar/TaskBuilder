using System.Collections.Generic;
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

        public string Value { get; }

        public InputValueModel DefaultValue { get; }

        public IEnumerable<InputValueModel> ValueOptions { get; }

        public InputModel(string name, string fullName, string typeName, InputAttribute attribute)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = TaskBuilderHelper.GetDisplayName(attribute.DisplayName, fullName, name);
            DisplayColor = TaskBuilderHelper.GetDisplayColor(typeName);
            InputType = attribute.InputType;
            DefaultValue = attribute.DefaultValue;
            ValueOptions = attribute.ValueOptions;
        }
    }
}