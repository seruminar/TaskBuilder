namespace TaskBuilder.Models.Function
{
    public class InputModel : IInputModel<InputFieldsModel>
    {
        public string Name { get; }

        public string TypeName { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public string DisplayColor { get; }

        public InputType InputType { get; set; } = InputType.Plain;

        public InputFieldsModel FieldsModel { get; set; }

        public InputFieldsModel DefaultFieldsModel { get; set; }

        public InputModel(string name, string typeName, string displayName, string description, string displayColor)
        {
            Name = name;
            TypeName = typeName;
            DisplayName = displayName;
            Description = description;
            DisplayColor = displayColor;
        }
    }
}