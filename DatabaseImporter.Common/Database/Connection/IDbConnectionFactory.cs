using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DatabaseImporter.Common.Database.Connection
{
    public interface IDbConnectionFactory
    {
        Task<SqlConnection> Create(IDbConnectionConfigurator connectionConfigurator);
    }
}