using System;

using TaskBuilder.Attributes;

namespace TaskBuilder.Functions.Implementations
{
    [Function("Start", 255, 0, 0)]
    public struct StartFunction : IInvokable, IDispatcher1
    {
        // Encapsulates behavior of Function
        public void Invoke()
        {
            // Receive parameters

            // Set up parameters

            // Send / Execute
            Dispatch1();
        }

        // This must be linked as a imperative delegate
        public Action Dispatch1 { get; set; }
    }
}