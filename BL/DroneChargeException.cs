﻿using System;
using System.Runtime.Serialization;

namespace IBL
{
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
}