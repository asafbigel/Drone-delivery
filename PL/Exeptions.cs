using System;
using System.Runtime.Serialization;

namespace PL
{

    [Serializable]
    internal class EnterPasswotdExeption : Exception
    {
        public EnterPasswotdExeption()
        {
        }

        public EnterPasswotdExeption(string message) : base(message)
        {
        }

        public EnterPasswotdExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EnterPasswotdExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    internal class WrongUserNameExeption : Exception
    {
        public WrongUserNameExeption()
        {
        }

        public WrongUserNameExeption(string message) : base(message)
        {
        }

        public WrongUserNameExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongUserNameExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
