using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Database.Table;
using DbColumn = DatabaseImporter.Common.Database.Table.DbColumn;

namespace DatabaseImporter.Database.Table
{
    public class MsSqlTableServices : ITableServices
    {
        private IDbConnectionConfigurator _configurator;
        private IDbConnectionFactory _dbConnectionFactory;
        public MsSqlTableServices(IDbConnectionConfigurator configurator,
            IDbConnectionFactory dbConnectionFactory)
        {
            _configurator = configurator;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<Catalog>> GetCatalogs()
        {
            const int tableDatabaseId = 0;
            const int tableDatabaseName= 1;

            using (var connection = await MakeConnection())
            using (var command = new SqlCommand("SELECT database_id,name FROM sys.databases;", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                var list = new List<Catalog>();
                while (await reader.ReadAsync())
                {
                    list.Add(new Catalog(reader.GetInt32(tableDatabaseId),reader.GetString(tableDatabaseName)));
                }

                return list;
            }
        }

        public async Task<List<DbSchema>> GetSchemas()
        {
            const int tableCatalog = 1;
            const int tableSchema = 0;

            using (var connection = await MakeConnection())
            using (var command = new SqlCommand("SELECT DISTINCT TABLE_SCHEMA,TABLE_CATALOG FROM INFORMATION_SCHEMA.Tables;", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                var list = new List<DbSchema>();
                while (await reader.ReadAsync())
                {
                    list.Add(new DbSchema(new Catalog(reader.GetString(tableCatalog)), reader.GetString(tableSchema)));
                }

                return list;
            }
        }
        
        public async Task<List<DbTable>> GetTables()
        {
            const int tableCatalog = 0;
            const int tableSchema = 1;
            const int tableName = 2;
            using (var connection = await MakeConnection())
            using (var command = new SqlCommand("SELECT TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME " +
                                                "FROM INFORMATION_SCHEMA.Tables " +
                                                "WHERE TABLE_TYPE = 'BASE TABLE';", connection))
            {
                var reader = await command.ExecuteReaderAsync();
                var list = new List<DbTable>();
                while (await reader.ReadAsync())
                {
                    var dbSchema = new DbSchema(new Catalog(reader.GetString(tableCatalog)), reader.GetString(tableSchema));
                    list.Add(new DbTable(dbSchema,reader.GetString(tableName)));
                }

                return list;
            }
        }

        public Task<List<DbTable>> GetTables(DbSchema schema)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<DbColumn>> GetDbColumns(DbTable table)
        {
            throw new System.NotImplementedException();
        }


        private async Task<SqlConnection> MakeConnection()
        {
            var connection = (await _dbConnectionFactory.Create(_configurator)) as SqlConnection;

            if (connection == null)
            {
                throw new InvalidDbConnectionException(typeof(SqlConnection));
            }

            await connection.OpenAsync();
            return connection;
        }
    }
}