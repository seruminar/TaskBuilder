using System;

namespace TaskBuilder.Attributes
{
    public class PortAttribute : Attribute
    {
        internal string DisplayName { get; }

        internal string Description { get; }

        protected PortAttribute(string displayName, string description)
        {
            DisplayName = displayName;
            Description = description;
        }
    }
}