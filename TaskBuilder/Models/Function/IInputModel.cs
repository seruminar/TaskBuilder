using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IInputModel : IParameterModel
    {
        InputType InputType { get; }

        string Value { get; }

        InputValueModel DefaultValue { get; }

        IEnumerable<InputValueModel> ValueOptions { get; }
    }
}