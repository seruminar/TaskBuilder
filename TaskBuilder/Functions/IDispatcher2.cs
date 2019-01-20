using System;

namespace TaskBuilder.Functions
{
    public interface IDispatcher2 : IDispatcher
    {
        Action Dispatch2 { get; set; }
    }
}