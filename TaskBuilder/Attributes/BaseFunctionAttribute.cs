using System;

namespace TaskBuilder.Attributes
{
    public class BaseFunctionAttribute : Attribute
    {
        public string DisplayName { get; }

        public BaseFunctionAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}