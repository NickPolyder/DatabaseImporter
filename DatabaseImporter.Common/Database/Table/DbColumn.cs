using System;

namespace DatabaseImporter.Common.Database.Table
{
    public struct DbColumn
    {
        public string Name { get; }
        public Type Type { get; }
        public int Size { get; set; }
        public string Description { get; set; }

        public DbColumn(string name,Type type,int? size=null,string description = null)
        {
            Name = name;
            Type = type;
            Size = size.GetValueOrDefault(0);
            Description = description ?? string.Empty;
        }
        
    }
}