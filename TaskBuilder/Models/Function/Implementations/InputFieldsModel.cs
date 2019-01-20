using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class InputFieldsModel : IInputFieldsModel<FieldModel>
    {
        public ICollection<FieldModel> Fields { get; }

        public InputFieldsModel(ICollection<FieldModel> fields)
        {
            Fields = fields;
        }
    }
}