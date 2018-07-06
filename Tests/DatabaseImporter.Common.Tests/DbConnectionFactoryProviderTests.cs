using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.Common.Tests.TestModels;
using Moq;
using Shouldly;
using Xunit;

namespace DatabaseImporter.Common.Tests
{
    public class DbConnectionFactoryProviderTests
    {
        [Fact]
        public void Register_Db_Connection_Factory_And_Find()
        {
            var dbConnectionName = new DbConnectionName("name");
            var provider = new DbConnectionFactoryProvider();
            var mockFactory = new Mock<IDbConnectionFactory>();
            mockFactory.Verify(tt => tt.Create(null), Times.AtMost(1));
            provider.Add(dbConnectionName, mockFactory.Object);
            provider.Create(dbConnectionName).Create(null);
            mockFactory.VerifyAll();
        }

        [Fact]
        public void Call_Unavailable_Db_Connection_Factory()
        {
            var dbConnectionName = new DbConnectionName("name");
            var callDbConnName = new DbConnectionName("SomeName");
            var provider = new DbConnectionFactoryProvider();
            var mockFactory = new Mock<IDbConnectionFactory>();
            provider.Add(dbConnectionName, mockFactory.Object);
            Should.Throw<DbConnectionFactoryProviderException>(() => provider.Create(callDbConnName));
        }
    }
}