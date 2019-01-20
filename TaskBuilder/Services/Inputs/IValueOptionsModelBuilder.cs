using TaskBuilder.Models.Function;

namespace TaskBuilder.Services.Inputs
{
    public interface IValueOptionsModelBuilder<TValue> : IValueBuilder<TValue>
    {
        InputFieldsModel OptionsModelFrom(params dynamic[] valueParams);
    }
}