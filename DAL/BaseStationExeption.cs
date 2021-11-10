using System;
using System.Runtime.Serialization;

namespace IDAL
{
    [Serializable]
    public class BaseStationExeption : Exception
    {
        public BaseStationExeption()
        {
        }

        public BaseStationExeption(string message) : base(message)
        {
        }

        public BaseStationExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BaseStationExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}