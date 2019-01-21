using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IInputFieldsModel<TInner> where TInner : IFieldModel
    {
        IEnumerable<TInner> Fields { get; }
    }
}