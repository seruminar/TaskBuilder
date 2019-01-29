using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IParameterModel : INamedPortModel
    {
        ICollection<string> TypeNames { get; }

        string DisplayColor { get; }
    }
}