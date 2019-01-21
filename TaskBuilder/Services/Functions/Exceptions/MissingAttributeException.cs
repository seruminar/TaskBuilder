using System;

namespace TaskBuilder.Services.Functions.Exceptions
{
    [Serializable]
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string message) : base(message)
        {
        }
    }
}