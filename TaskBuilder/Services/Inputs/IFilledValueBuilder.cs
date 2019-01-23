using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Services.Inputs
{
    public interface IFilledValueBuilder<TValue> : IValueBuilder<TValue>
    {
        IInputValueModel GetFilledModel(IInputValueModel structureModel, params dynamic[] filledModelParams);
    }
}