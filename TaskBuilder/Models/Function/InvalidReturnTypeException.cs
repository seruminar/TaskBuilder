using System;

namespace TaskBuilder.Models.Function
{
    [Serializable]
    public class InvalidReturnTypeException : Exception
    {
        public InvalidReturnTypeException(string message) : base(message)
        {
        }
    }
}