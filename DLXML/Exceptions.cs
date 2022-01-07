using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    internal class XMLFileLoadCreateException : Exception
    {
        private string filePath;
        private string v;
        private Exception ex;

        public XMLFileLoadCreateException()
        {
        }

        public XMLFileLoadCreateException(string message) : base(message)
        {
        }

        public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public XMLFileLoadCreateException(string filePath, string v, Exception ex)
        {
            this.filePath = filePath;
            this.v = v;
            this.ex = ex;
        }

        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class BadDroneIdException : Exception
    {
        private int id;
        private string v;

        public BadDroneIdException()
        {
        }

        public BadDroneIdException(string message) : base(message)
        {
        }

        public BadDroneIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadDroneIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadDroneIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class BadBaseStationIdException : Exception
    {
        private int id;
        private string v;

        public BadBaseStationIdException()
        {
        }

        public BadBaseStationIdException(string message) : base(message)
        {
        }

        public BadBaseStationIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadBaseStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadBaseStationIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class BadParcelIdException : Exception
    {
        private int id;
        private string v;

        public BadParcelIdException()
        {
        }

        public BadParcelIdException(string message) : base(message)
        {
        }

        public BadParcelIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadParcelIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadParcelIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class BadCustomerIdException : Exception
    {
        private int id;
        private string v;

        public BadCustomerIdException()
        {
        }

        public BadCustomerIdException(string message) : base(message)
        {
        }

        public BadCustomerIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadCustomerIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadCustomerIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    internal class BadDroneChargeIdException : Exception
    {
        private int id;
        private string v;

        public BadDroneChargeIdException()
        {
        }

        public BadDroneChargeIdException(string message) : base(message)
        {
        }

        public BadDroneChargeIdException(int id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadDroneChargeIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadDroneChargeIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}