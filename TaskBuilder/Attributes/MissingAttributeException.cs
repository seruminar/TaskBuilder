using System;

namespace TaskBuilder.Attributes
{
    [Serializable]
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string message) : base(message)
        {
        }
    }
}