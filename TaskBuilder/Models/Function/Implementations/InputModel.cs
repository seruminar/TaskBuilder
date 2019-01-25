﻿using Newtonsoft.Json;
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

        public bool InlineOnly { get; }

        public InputType InputType { get; set; } = InputType.Plain;

        public IInputValueModel StructureModel { get; set; }

        public IInputValueModel FilledModel { get; set; }

        public InputModel(string name, string typeName, string displayName, string description, string displayColor, bool inlineOnly)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
            InlineOnly = inlineOnly;
        }

        [JsonConstructor()]
        public InputModel(string name, string typeName, string displayName, string description, string displayColor, bool inlineOnly, InputType inputType, InputValueModel structureModel, InputValueModel filledModel) : this(name, typeName, displayName, description, displayColor, inlineOnly)
        {
            InputType = inputType;
            StructureModel = structureModel;
            FilledModel = filledModel;
        }
    }
}