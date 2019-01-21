using System;

namespace TaskBuilder.Services.Functions.Exceptions
{
    [Serializable]
    public class InvalidReturnTypeException : Exception
    {
        public InvalidReturnTypeException(string message) : base(message)
        {
        }
    }
}