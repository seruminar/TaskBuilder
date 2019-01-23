using System;

namespace TaskBuilder.Models.Function.InputValue
{
    public class FieldModel : IFieldModel
    {
        public FieldType Type { get; }

        public FieldParameter[] Value { get; set; }

        public FieldParameter this[int index] => Value[index];

        public FieldModel(FieldType type, dynamic value = null)
        {
            Type = type;

            if (value is Array)
            {
                Value = value;
            }
            else
            {
                Value = new FieldParameter[] { value };
            }
        }
    }
}