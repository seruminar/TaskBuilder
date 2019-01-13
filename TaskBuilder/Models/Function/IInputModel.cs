namespace TaskBuilder.Models.Function
{
    public interface IInputModel : IParameterModel
    {
        InputType InputType { get; }

        object DefaultValue { get; }

        InputOptionModel[] InputOptions { get; }
    }
}