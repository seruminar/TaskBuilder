using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Method,
               AllowMultiple = false,
               Inherited = true)]
    public class InReceiverAttribute : BaseTaskActionAttribute
    {
        public InReceiverAttribute()
        {
        }
    }
}