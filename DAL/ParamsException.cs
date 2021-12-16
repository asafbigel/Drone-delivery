using System;
using System.Runtime.Serialization;

namespace DalObject
{
    [Serializable]
    internal class ParamsException : Exception
    {
        public ParamsException()
        {
        }

        public ParamsException(string message) : base(message)
        {
        }

        public ParamsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParamsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}