using System.Threading.Tasks;

namespace DatabaseImporter.Common.Database.Connection
{
    public interface IDbConnectionConfigurator
    {
        Task<bool> ConnectionStringExists();

        Task<string> LoadConnectionString();

        Task WriteConnectionString(string connectionString);
    }
}