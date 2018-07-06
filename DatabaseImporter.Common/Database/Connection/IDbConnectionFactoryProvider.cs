namespace DatabaseImporter.Common.Database.Connection
{
    public interface IDbConnectionFactoryProvider
    {
        bool TryCreateFactory(DbConnectionName key, out IDbConnectionFactory value);

        IDbConnectionFactory Create(DbConnectionName name);
    }
}