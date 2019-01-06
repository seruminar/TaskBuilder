using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IFunctionModel : IBaseModel
    {
        ITypedModel Enter { get; }

        ITypedModel Leave { get; }

        ICollection<IColoredModel> Inputs { get; }

        ICollection<IColoredModel> Outputs { get; }
    }
}