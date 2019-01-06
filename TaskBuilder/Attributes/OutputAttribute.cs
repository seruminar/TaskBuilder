using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property,
              AllowMultiple = false,
              Inherited = true)]
    public class OutputAttribute : ColoredAttribute
    {
        public OutputAttribute() : base(null)
        {
        }

        public OutputAttribute(string displayName) : base(displayName)
        {
        }
    }
}