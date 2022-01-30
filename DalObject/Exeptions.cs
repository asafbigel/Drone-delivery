using System;
using System.Runtime.Serialization;

namespace Dal
{
    [Serializable]
    internal class DroneChargeExeption : Exception
    {
        public DroneChargeExeption()
        {
        }

        public DroneChargeExeption(string message) : base(message)
        {
        }

        public DroneChargeExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DroneChargeExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
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
    [Serializable]
    internal class DroneExeption : Exception
    {
        public DroneExeption()
        {
        }

        public DroneExeption(string message) : base(message)
        {
        }

        public DroneExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DroneExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
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
    [Serializable]
    internal class BaseStationExeption : Exception
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