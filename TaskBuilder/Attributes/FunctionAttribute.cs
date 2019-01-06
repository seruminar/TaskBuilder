using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class,
               AllowMultiple = false,
               Inherited = true)]
    public class FunctionAttribute : NamedAttribute
    {
        public FunctionAttribute(string displayName = null) : base(displayName)
        {
        }
    }
}