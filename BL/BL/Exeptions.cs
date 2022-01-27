using System;
using System.Runtime.Serialization;

namespace BL
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
    [Serializable]
    internal class slotException : Exception
    {
        public slotException()
        {
        }

        public slotException(string message) : base(message)
        {
        }

        public slotException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected slotException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class DroneChargeException : Exception
    {
        public DroneChargeException()
        {
        }

        public DroneChargeException(string message) : base(message)
        {
        }

        public DroneChargeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DroneChargeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class ParcelException : Exception
    {
        public ParcelException()
        {
        }

        public ParcelException(string message) : base(message)
        {
        }

        public ParcelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParcelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class DateException : Exception
    {
        public DateException()
        {
        }

        public DateException(string message) : base(message)
        {
        }

        public DateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DateException(SerializationInfo info, StreamingContext context) : base(info, context)
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
    [Serializable]
    internal class DroneBatteryException : Exception
    {
        public DroneBatteryException()
        {
        }

        public DroneBatteryException(string message) : base(message)
        {
        }

        public DroneBatteryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DroneBatteryException(SerializationInfo info, StreamingContext context) : base(info, context)
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
    [Serializable]
    internal class NotNeedToArrivedException : Exception
    {
        public NotNeedToArrivedException()
        {
        }

        public NotNeedToArrivedException(string message) : base(message)
        {
        }

        public NotNeedToArrivedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotNeedToArrivedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}