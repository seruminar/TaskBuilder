using Newtonsoft.Json.Linq;

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

            if (value is JArray)
            {
                Value = value.ToObject<FieldParameter[]>();
            }
            else
            {
                Value = new FieldParameter[] { value };
            }
        }
    }
}