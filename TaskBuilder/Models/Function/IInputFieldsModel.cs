using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IInputFieldsModel<TInner> where TInner : IFieldModel
    {
        ICollection<TInner> Fields { get; }
    }
}