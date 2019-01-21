using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public interface IFunctionModel<TInvoke, TDispatch, TInput, TOutput> where TInvoke : IInvokeModel
                                                                        where TDispatch : IDispatchModel
                                                                        where TInput : IInputModel<InputFieldsModel>
                                                                        where TOutput : IOutputModel
    {
        Guid TypeIdentifier { get; }

        string DisplayName { get; }

        string DisplayColor { get; }

        TInvoke Invoke { get; }

        ICollection<TDispatch> Dispatchs { get; }

        ICollection<TInput> Inputs { get; }

        ICollection<TOutput> Outputs { get; }
    }
}