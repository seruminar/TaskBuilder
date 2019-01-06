using System;

namespace TaskBuilder.Models.Function
{
    [Serializable]
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string message) : base(message)
        {
        }
    }
}