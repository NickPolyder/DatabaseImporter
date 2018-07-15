namespace DatabaseImporter.Common.Database.Table
{
    public struct Catalog
    {
        public int Id { get; }

        public string Name { get; }

        public Catalog(string name) : this(-1, name)
        { }

        public Catalog(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
    }
}