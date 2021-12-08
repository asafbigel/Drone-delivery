using System;
using System.Runtime.Serialization;

namespace ConsuleUI_BL
{
    [Serializable]
    internal class InputException : Exception
    {
        public InputException()
        {
        }

        public InputException(string message) : base(message)
        {
        }

        public InputException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
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