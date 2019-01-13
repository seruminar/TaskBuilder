using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OutputAttribute : PortAttribute
    {
        public OutputAttribute(string displayName = null)
        {
            DisplayName = displayName;
        }
    }
}