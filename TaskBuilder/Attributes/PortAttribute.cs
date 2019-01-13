using System;

namespace TaskBuilder.Attributes
{
    public class PortAttribute : Attribute
    {
        public string DisplayName { get; protected set; }
    }
}