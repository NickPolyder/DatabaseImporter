using System;
using System.Runtime.Serialization;

namespace DatabaseImporter.Common.Database.Connection
{
    public class DbConnectionFactoryProviderException : Exception
    {
        public DbConnectionName? ConnectionName
        {
            get => Data[nameof(ConnectionName)] as DbConnectionName?;
            set => Data[nameof(ConnectionName)] = value;
        }

        public DbConnectionFactoryProviderException()
        {
        }

        public DbConnectionFactoryProviderException(DbConnectionName dbConnName):this($"Connection Provider for {dbConnName.Name} not found")
        {
            ConnectionName = dbConnName;
        }

        public DbConnectionFactoryProviderException(string message) : base(message)
        {
        }

        public DbConnectionFactoryProviderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbConnectionFactoryProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}