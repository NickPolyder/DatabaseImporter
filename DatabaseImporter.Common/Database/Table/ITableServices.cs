using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseImporter.Common.Database.Table
{
    public interface ITableServices
    {
        Task<List<Catalog>> GetCatalogs();

        Task<List<DbSchema>> GetSchemas();

        Task<List<DbTable>> GetTables();

        Task<List<DbTable>> GetTables(DbSchema schema);

        Task<List<DbColumn>> GetDbColumns(DbTable table);

    }
}