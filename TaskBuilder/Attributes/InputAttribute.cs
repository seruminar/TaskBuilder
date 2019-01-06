using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property,
               AllowMultiple = false,
               Inherited = true)]
    public class InputAttribute : ColoredAttribute
    {
        public InputAttribute() : base(null)
        {
        }

        public InputAttribute(string displayName) : base(displayName)
        {
        }
    }
}