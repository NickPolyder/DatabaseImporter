using System;
using System.Runtime.Serialization;

namespace DatabaseImporter.Common.Database.Connection
{
    public class ConnectionStringNotFoundException : Exception
    {
        public ConnectionStringNotFoundException():this("The ConnectionString could not be loaded.")
        {
        }

        public ConnectionStringNotFoundException(string message) : base(message)
        {
        }

        public ConnectionStringNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConnectionStringNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}