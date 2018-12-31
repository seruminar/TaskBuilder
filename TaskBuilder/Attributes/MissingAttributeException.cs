using System;

namespace TaskBuilder.Models
{
    [Serializable]
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string message) : base(message)
        {
        }
    }
}