namespace TaskBuilder.Models.Function
{
    public interface IInputModel<TFieldsModel> : IParameterModel where TFieldsModel : IInputFieldsModel<FieldModel>
    {
        InputType InputType { get; }

        TFieldsModel FieldsModel { get; }

        TFieldsModel DefaultFieldsModel { get; }
    }
}