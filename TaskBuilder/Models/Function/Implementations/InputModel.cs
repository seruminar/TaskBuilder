using System.Collections.Generic;
using Newtonsoft.Json;

using TaskBuilder.Functions;
using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Models.Function
{
    public class InputModel : IInputModel
    {
        public string Name { get; }

        public ICollection<string> TypeNames { get; set; }

        public string DisplayName { get; }

        public string Description { get; }

        public string DisplayColor { get; }

        public bool InlineOnly { get; }

        public InputType InputType { get; set; } = InputType.Plain;

        public IInputValueModel StructureModel { get; set; }

        public IInputValueModel FilledModel { get; set; }

        [JsonConstructor]
        public InputModel(string name, string displayName, string description, string displayColor, bool inlineOnly, InputType inputType, InputValueModel structureModel, InputValueModel filledModel) : this(name, displayName, description, displayColor, inlineOnly)
        {
            InputType = inputType;
            StructureModel = structureModel;
            FilledModel = filledModel;
        }

        public InputModel(string name, string displayName, string description, string displayColor, bool inlineOnly)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
            InlineOnly = inlineOnly;
        }
    }
}