using Newtonsoft.Json;
using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Models.Function
{
    public class InputModel : IInputModel
    {
        public string Name { get; }

        public string TypeName { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public string DisplayColor { get; }

        public InputType InputType { get; set; } = InputType.Plain;

        public IInputValueModel StructureModel { get; set; }

        public IInputValueModel FilledModel { get; set; }

        public InputModel(string name, string typeName, string displayName, string description, string displayColor)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
        }

        [JsonConstructor()]
        public InputModel(string name, string typeName, string displayName, string description, string displayColor, InputType inputType, InputValueModel structureModel, InputValueModel filledModel) : this(name, typeName, displayName, description, displayColor)
        {
            InputType = inputType;
            StructureModel = structureModel;
            FilledModel = filledModel;
        }
    }
}