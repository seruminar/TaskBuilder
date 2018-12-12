using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Method,
               AllowMultiple = false,
               Inherited = true)]
    public class EnterAttribute : BaseFunctionAttribute
    {
        public EnterAttribute()
        {
        }
    }
}