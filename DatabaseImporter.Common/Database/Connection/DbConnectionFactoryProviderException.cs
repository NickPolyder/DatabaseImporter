using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DatabaseImporter.Common.Database.Connection
{
    public class DbConnectionFactoryProviderException : Exception
    {
        public DbConnectionName? ConnectionName
        {
            get
            {
                var str = Data[nameof(ConnectionName)]?.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    return JsonConvert.DeserializeObject<DbConnectionName?>(str);
                }

                return null;
            }
            set => Data[nameof(ConnectionName)] = value != null ? JsonConvert.SerializeObject(value) : null;
        }

        public DbConnectionFactoryProviderException()
        {
        }

        public DbConnectionFactoryProviderException(DbConnectionName dbConnName) : this($"Connection Provider for {dbConnName.Name} not found")
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