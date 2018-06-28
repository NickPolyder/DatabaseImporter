using System.Threading.Tasks;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.WPF.Properties;

namespace DatabaseImporter.WPF.Infastructure.Database.Connection
{
    public class AppSettingsDatabaseConfigurator : IDbConnectionConfigurator
    {
        public Task<bool> ConnectionStringExists()
        {
            return Task.FromResult(!string.IsNullOrEmpty(Settings.Default.ConnectionString));
        }

        public Task<string> LoadConnectionString()
        {
            return Task.FromResult(Settings.Default.ConnectionString);
        }

        public Task WriteConnectionString(string connectionString)
        {
            Settings.Default.ConnectionString = connectionString;
            return Task.CompletedTask;
        }
    }
}