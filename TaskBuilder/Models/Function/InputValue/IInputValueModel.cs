using System.Collections.Generic;

namespace TaskBuilder.Models.Function.InputValue
{
    public interface IInputValueModel
    {
        IDictionary<string, IFieldModel> Fields { get; }
    }
}