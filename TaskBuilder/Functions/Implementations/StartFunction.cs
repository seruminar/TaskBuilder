using System;

using TaskBuilder.Attributes;

namespace TaskBuilder.Functions.Implementations
{
    [Function("Start", 255, 0, 0)]
    public struct StartFunction : IInvokable, IDispatcher
    {
        // Encapsulates behavior of Function
        public void Invoke()
        {
            // Receive parameters

            // Set up parameters

            // Send / Execute
            Dispatch();
        }

        // This must be linked as a imperative delegate
        public Action Dispatch { get; set; }
    }
}