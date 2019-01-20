using TaskBuilder.Models.Function;

namespace TaskBuilder.Services.Inputs
{
    public interface IValueModelBuilder<TValue> : IValueBuilder<TValue>
    {
        InputFieldsModel FieldsModelFrom(params dynamic[] fieldsParams);
    }
}