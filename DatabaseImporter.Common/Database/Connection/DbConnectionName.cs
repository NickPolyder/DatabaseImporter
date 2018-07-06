using System;

namespace DatabaseImporter.Common.Database.Connection
{
    public struct DbConnectionName:IEquatable<DbConnectionName>
    {
        public Guid Id { get; }
        public string Name { get; }

        public DbConnectionName(string name) : this(Guid.NewGuid(), name)
        { }

        public DbConnectionName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(DbConnectionName other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DbConnectionName && Equals((DbConnectionName) obj);
        }

        public override int GetHashCode()
        {
            return (Id.GetHashCode());
        }

        public static bool operator ==(DbConnectionName left, DbConnectionName right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DbConnectionName left, DbConnectionName right)
        {
            return !(left == right);
        }
    }
}