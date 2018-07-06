using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace DatabaseImporter.Common.Database.Connection
{
    public class DbConnectionFactoryProvider : IDbConnectionFactoryProvider,IEnumerable<KeyValuePair<DbConnectionName, IDbConnectionFactory>>
    {
        private IDictionary<DbConnectionName, IDbConnectionFactory> _factoryItems;

        /// <inheritdoc />
        public DbConnectionFactoryProvider()
        {
            _factoryItems = new Dictionary<DbConnectionName, IDbConnectionFactory>(DbConnectionNameComparer.Default);
        }

        public IDbConnectionFactory Create(DbConnectionName name)
        {
            if (TryCreateFactory(name, out IDbConnectionFactory value))
            {
                return value;
            }
            throw new DbConnectionFactoryProviderException(name);
        }

        #region IEnumerable
        public IEnumerator<KeyValuePair<DbConnectionName, IDbConnectionFactory>> GetEnumerator()
        {
            return _factoryItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public void Add(KeyValuePair<DbConnectionName, IDbConnectionFactory> item)
        {
            _factoryItems.Add(item);
        }
        public void Add(DbConnectionName key, IDbConnectionFactory value)
        {
            _factoryItems.Add(key, value);
        }
        
        public bool TryCreateFactory(DbConnectionName key, out IDbConnectionFactory value)
        {
            return _factoryItems.TryGetValue(key, out value);
        }
        
    }
}