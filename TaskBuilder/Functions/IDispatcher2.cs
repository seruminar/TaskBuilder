using System;

namespace TaskBuilder.Functions
{
    public interface IDispatcher2 : IDispatcher1
    {
        Action Dispatch2 { get; set; }
    }
}