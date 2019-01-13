using System;

namespace TaskBuilder.Models.Function.Exceptions
{
    [Serializable]
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string message) : base(message)
        {
        }
    }
}