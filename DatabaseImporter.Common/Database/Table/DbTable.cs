using System;

namespace DatabaseImporter.Common.Database.Table
{
    public struct DbTable
    {
        public DbSchema Schema { get;  }

        public string Name { get; }

        public DbColumn[] Columns { get; }

        public DbTable(string schema,string name):this((DbSchema)schema,name,Array.Empty<DbColumn>())
        { }

        public DbTable(DbSchema schema, string name) : this(schema, name, Array.Empty<DbColumn>())
        { }

        public DbTable(DbSchema schema, string name, DbColumn[] columns)
        {
            Schema = schema;
            Name = name;
            Columns = columns;
        }
    }
}