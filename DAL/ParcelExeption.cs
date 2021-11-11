using System;
using System.Runtime.Serialization;

namespace DalObject
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
}