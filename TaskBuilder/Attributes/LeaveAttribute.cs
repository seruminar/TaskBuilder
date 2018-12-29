using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property,
              AllowMultiple = false,
              Inherited = true)]
    public class LeaveAttribute : BaseFunctionAttribute
    {
        public LeaveAttribute() : base(null)
        {
        }

        public LeaveAttribute(string displayName) : base(displayName)
        {
        }
    }
}