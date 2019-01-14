using System;

namespace TaskBuilder.Attributes
{
    public class PortAttribute : Attribute
    {
        internal string DisplayName { get; set; }
    }
}