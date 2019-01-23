using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Services.Inputs
{
    public interface IValueBuilder<TValue>
    {
        TValue BuildValue(IInputValueModel inputValueModel);

        IInputValueModel GetStructureModel(params dynamic[] structureModelParams);
    }
}