using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class InputModel : IInputModel
    {
        public string Name { get; }

        public string TypeName { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public string DisplayColor { get; }

        public InputType InputType { get; }

        public IInputValueModel DefaultValue { get; }

        public IEnumerable<IInputValueModel> ValueOptions { get; }

        public InputModel(string name, string typeName, string displayName, string description, string displayColor, InputType inputType, IInputValueModel defaultValue, IEnumerable<IInputValueModel> valueOptions)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
            InputType = inputType;
            DefaultValue = defaultValue;
            ValueOptions = valueOptions;
        }
    }
}