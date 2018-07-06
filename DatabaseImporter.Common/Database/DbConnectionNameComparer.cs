using System.Collections.Generic;
using DatabaseImporter.Common.Database.Connection;

namespace DatabaseImporter.Common.Database
{
    public class DbConnectionNameComparer : IEqualityComparer<DbConnectionName>
    {
        private static readonly object Lock = new object();
        private static volatile IEqualityComparer<DbConnectionName> _default;

        public static IEqualityComparer<DbConnectionName> Default
        {
            get
            {
                if (_default == null)
                {
                    lock (Lock)
                    {
                        if (_default == null)
                        {
                            _default = new DbConnectionNameComparer();
                        }
                    }
                }

                return _default;
            }
        }
        public bool Equals(DbConnectionName x, DbConnectionName y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(DbConnectionName obj)
        {
            return obj.GetHashCode();
        }
    }
}