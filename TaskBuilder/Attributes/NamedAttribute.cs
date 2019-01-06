using System;

namespace TaskBuilder.Attributes
{
    public class NamedAttribute : Attribute
    {
        public string DisplayName { get; }

        public NamedAttribute(string displayName = null)
        {
            DisplayName = displayName;
        }
    }
}