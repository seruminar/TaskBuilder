using System;
using System.Collections.Generic;
using TaskBuilder.Functions;

namespace TaskBuilder.Tasks
{
    public class Task
    {
        private readonly Dictionary<Guid, IInvokable> _invokables;
        private IInvokable _startInvokable;

        public Task(Dictionary<Guid, IInvokable> invokables, IInvokable startInvokable)
        {
            _invokables = invokables;
            _startInvokable = startInvokable;
        }

        internal void Invoke()
        {
            // Call the start function
            _startInvokable.Invoke();
        }
    }
}