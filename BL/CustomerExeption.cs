using System;
using System.Runtime.Serialization;

namespace IBL
{
    [Serializable]
    internal class CustomerExeption : Exception
    {
        public CustomerExeption()
        {
        }

        public CustomerExeption(string message) : base(message)
        {
        }

        public CustomerExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}