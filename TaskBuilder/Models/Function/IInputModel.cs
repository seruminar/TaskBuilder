using TaskBuilder.Functions;
using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Models.Function
{
    public interface IInputModel : IParameterModel
    {
        bool InlineOnly { get; }

        InputType InputType { get; }

        IInputValueModel StructureModel { get; }

        IInputValueModel FilledModel { get; }
    }
}