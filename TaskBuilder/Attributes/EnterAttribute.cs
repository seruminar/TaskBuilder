using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Method,
               AllowMultiple = false,
               Inherited = true)]
    public class EnterAttribute : ColoredAttribute
    {
        public EnterAttribute() : base(null)
        {
        }

        public EnterAttribute(string displayName) : base(displayName)
        {
        }
    }
}