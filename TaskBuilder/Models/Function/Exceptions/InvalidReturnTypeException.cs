using System;

namespace TaskBuilder.Models.Function.Exceptions
{
    [Serializable]
    public class InvalidReturnTypeException : Exception
    {
        public InvalidReturnTypeException(string message) : base(message)
        {
        }
    }
}