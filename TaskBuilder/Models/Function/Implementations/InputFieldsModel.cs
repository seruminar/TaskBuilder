using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class InputFieldsModel : IInputFieldsModel<FieldModel>
    {
        public IEnumerable<FieldModel> Fields { get; }

        public InputFieldsModel(IEnumerable<FieldModel> fields)
        {
            Fields = fields;
        }
    }
}