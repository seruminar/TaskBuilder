using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IInputModel : IParameterModel
    {
        InputType InputType { get; }

        IInputValueModel DefaultValue { get; }

        IEnumerable<IInputValueModel> ValueOptions { get; }
    }
}