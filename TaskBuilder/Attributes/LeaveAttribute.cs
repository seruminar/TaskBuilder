using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property,
              AllowMultiple = false,
              Inherited = true)]
    public class LeaveAttribute : ColoredAttribute
    {
        public LeaveAttribute() : base(null)
        {
        }

        public LeaveAttribute(string displayName) : base(displayName)
        {
        }
    }
}