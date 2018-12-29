using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property,
              AllowMultiple = false,
              Inherited = true)]
    public class OutputAttribute : BaseFunctionAttribute
    {
        public OutputAttribute() : base(null)
        {
        }

        public OutputAttribute(string displayName) : base(displayName)
        {
        }
    }
}