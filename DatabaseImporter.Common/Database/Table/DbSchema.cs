namespace DatabaseImporter.Common.Database.Table
{
    public struct DbSchema
    {
        public Catalog? Catalog { get; }
        public string Name { get; }

        public DbSchema(string name) : this(null, name)
        { }

        public DbSchema(Catalog? catalog, string name)
        {
            Name = name;
            Catalog = catalog;
        }
        
        public static explicit operator DbSchema(string schema)
        {
            return new DbSchema(schema);
        }


    }
}