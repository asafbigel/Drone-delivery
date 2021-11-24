using System;
using System.Runtime.Serialization;

namespace IBL
{
    [Serializable]
    internal class IntReadException : Exception
    {
        public IntReadException()
        {
        }

        public IntReadException(string message) : base(message)
        {
        }

        public IntReadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IntReadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}