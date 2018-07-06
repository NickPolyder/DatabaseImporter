using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace DatabaseImporter.Common.Database.Connection
{
    public class DbConnectionFactoryProvider : IDictionary<DbConnectionName, IDbConnectionFactory>
    {
        private IDictionary<DbConnectionName, IDbConnectionFactory> _factoryItems;

        /// <inheritdoc />
        public DbConnectionFactoryProvider()
        {
            _factoryItems = new Dictionary<DbConnectionName, IDbConnectionFactory>(DbConnectionNameComparer.Default);
        }

        public Task<DbConnection> Create(DbConnectionName name, IDbConnectionConfigurator connectionConfigurator)
        {
            if (_factoryItems.TryGetValue(name, out IDbConnectionFactory value))
            {
                return value.Create(connectionConfigurator);
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

        public void Clear()
        {
            _factoryItems.Clear();
        }

        public bool Contains(KeyValuePair<DbConnectionName, IDbConnectionFactory> item)
        {
            return _factoryItems.Contains(item);
        }

        public bool ContainsKey(DbConnectionName key)
        {
            return _factoryItems.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DbConnectionName, IDbConnectionFactory>[] array, int arrayIndex)
        {
            _factoryItems.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<DbConnectionName, IDbConnectionFactory> item)
        {
            return _factoryItems.Remove(item);
        }

        public int Count => _factoryItems.Count;
        public bool IsReadOnly => _factoryItems.IsReadOnly;

        public bool Remove(DbConnectionName key)
        {
            return _factoryItems.Remove(key);
        }

        public bool TryGetValue(DbConnectionName key, out IDbConnectionFactory value)
        {
            return _factoryItems.TryGetValue(key, out value);
        }

        public IDbConnectionFactory this[DbConnectionName key]
        {
            get => _factoryItems[key];
            set => _factoryItems[key] = value;
        }

        public ICollection<DbConnectionName> Keys => _factoryItems.Keys;
        public ICollection<IDbConnectionFactory> Values => _factoryItems.Values;
    }
}