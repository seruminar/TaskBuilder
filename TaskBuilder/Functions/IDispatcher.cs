using System;

namespace TaskBuilder.Functions
{
    public interface IDispatcher
    {
        Action Dispatch { get; set; }
    }
}