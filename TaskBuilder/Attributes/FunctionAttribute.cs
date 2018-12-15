using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class,
               AllowMultiple = false,
               Inherited = true)]
    public class FunctionAttribute : BaseFunctionAttribute
    {
        public FunctionAttribute()
        {
        }
    }
}