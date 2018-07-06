using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DatabaseImporter.Common.Database.Connection;

namespace DatabaseImporter.Database.Connection
{
    public class DefaultDbConnectionFactory : IDbConnectionFactory
    {
        private static readonly Lazy<IDbConnectionFactory> DefaultFactory = new Lazy<IDbConnectionFactory>(() => new DefaultDbConnectionFactory());

        public static IDbConnectionFactory Default => DefaultFactory.Value;

        public virtual async Task<DbConnection> Create(IDbConnectionConfigurator connectionConfigurator)
        {
            if (connectionConfigurator == null)
                throw new ArgumentNullException(nameof(connectionConfigurator));

            if (await connectionConfigurator.ConnectionStringExists())
            {
                throw new ConnectionStringNotFoundException();
            }

            var connectionString = await connectionConfigurator.LoadConnectionString();

            return new SqlConnection(connectionString);
        }
    }
}