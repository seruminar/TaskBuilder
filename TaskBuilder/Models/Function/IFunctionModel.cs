using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IFunctionModel
    {
        string TypeIdentifier { get; }

        string DisplayName { get; }

        string DisplayColor { get; }

        IInvokeModel Invoke { get; }

        ICollection<IDispatchModel> Dispatchs { get; }

        ICollection<IInputModel> Inputs { get; }

        ICollection<IOutputModel> Outputs { get; }
    }
}