using System;
using System.Runtime.Serialization;

namespace BO
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
    [Serializable]
    internal class DroneIdException : Exception
    {
        public DroneIdException()
        {
        }

        public DroneIdException(string message) : base(message)
        {
        }

        public DroneIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DroneIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }
    [Serializable]
    internal class CustomerIdExeption : Exception
    {
        public CustomerIdExeption()
        {
        }

        public CustomerIdExeption(string message) : base(message)
        {
        }

        public CustomerIdExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerIdExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }
    [Serializable]
    internal class BaseStationIdExeption : Exception
    {
        public BaseStationIdExeption()
        {
        }

        public BaseStationIdExeption(string message) : base(message)
        {
        }

        public BaseStationIdExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BaseStationIdExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }

    [Serializable]
    internal class CustomerAtParcelNullExeption : Exception
    {
        public CustomerAtParcelNullExeption()
        {
        }

        public CustomerAtParcelNullExeption(string message) : base(message)
        {
        }

        public CustomerAtParcelNullExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerAtParcelNullExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        [Serializable]
        internal class EnterPasswordExeption : Exception
        {
            public EnterPasswordExeption()
            {
            }

            public EnterPasswordExeption(string message) : base(message)
            {
            }

            public EnterPasswordExeption(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected EnterPasswordExeption(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }


        }
    }
    [Serializable]
    internal class SelfParcelExeption : Exception
    {
        public SelfParcelExeption()
        {
        }

        public SelfParcelExeption(string message) : base(message)
        {
        }

        public SelfParcelExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SelfParcelExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
   ;



    [Serializable]
    internal class NoFreeChargingException : Exception
    {
        public NoFreeChargingException()
        {
        }

        public NoFreeChargingException(string message) : base(message)
        {
        }

        public NoFreeChargingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFreeChargingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class BadDroneIdException : Exception
    {
        public int id;
        public string v;

        public BadDroneIdException()
        {
        }

        public BadDroneIdException(string message) : base(message)
        {
        }

        public BadDroneIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadDroneIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        protected BadDroneIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

