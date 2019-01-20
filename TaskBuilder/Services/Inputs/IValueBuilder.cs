using TaskBuilder.Models.Function;

namespace TaskBuilder.Services.Inputs
{
    public interface IValueBuilder<TValue>
    {
        TValue ConstructValue(InputFieldsModel inputValueModel);

        InputFieldsModel NewFieldsModel();
    }
}