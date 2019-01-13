using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IFunctionModel
    {
        string Name { get; }

        string DisplayName { get; }

        string DisplayColor { get; }

        IInvokeModel Invoke { get; }

        IDispatchModel Dispatch { get; }

        ICollection<IInputModel> Inputs { get; }

        ICollection<IOutputModel> Outputs { get; }
    }
}