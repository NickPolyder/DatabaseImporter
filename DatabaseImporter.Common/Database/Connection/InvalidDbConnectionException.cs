using System;
using System.Runtime.Serialization;

namespace DatabaseImporter.Common.Database.Connection
{
    public class InvalidDbConnectionException : Exception
    {
        public InvalidDbConnectionException() : this("The database connection is invalid.")
        {
        }

        public InvalidDbConnectionException(Type dbConnection) : this($"The database connection is not of type {dbConnection.Name}")
        {

        }
        public InvalidDbConnectionException(string message) : base(message)
        {
        }

        public InvalidDbConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDbConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}