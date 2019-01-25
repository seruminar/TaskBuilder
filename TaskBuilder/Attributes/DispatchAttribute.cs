using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DispatchAttribute : PortAttribute
    {
        public DispatchAttribute(string displayName = null, string description = null) : base(displayName, description)
        {
        }
    }
}