using TaskBuilder.Functions;

namespace TaskBuilder.Models.Function.InputValue
{
    public interface IFieldModel
    {
        FieldType Type { get; }

        FieldParameter[] Value { get; set; }

        FieldParameter this[int index] { get; }
    }
}