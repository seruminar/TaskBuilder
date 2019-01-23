using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Models.Function
{
    public interface IInputModel : IParameterModel
    {
        InputType InputType { get; }

        IInputValueModel StructureModel { get; }

        IInputValueModel FilledModel { get; }
    }
}