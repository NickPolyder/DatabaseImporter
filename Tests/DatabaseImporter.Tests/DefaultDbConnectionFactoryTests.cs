using System;
using DatabaseImporter.Common.Database.Connection;
using DatabaseImporter.Database.Connection;
using Moq;
using Shouldly;
using Xunit;

namespace DatabaseImporter.Tests
{
    public class DefaultDbConnectionFactoryTests
    {
        [Fact]
        public void Should_Throw_If_The_Connection_String_Does_Not_Exist()
        {
            var mock = new Moq.Mock<IDbConnectionConfigurator>();
            mock.Setup(tt => tt.ConnectionStringExists()).ReturnsAsync(true);

            var factory = new DefaultDbConnectionFactory();

            factory.Create(mock.Object).ShouldThrowAsync(typeof(ConnectionStringNotFoundException));
        }

        [Fact]
        public void Should_Return_Empty_SqlConnection()
        {
            var mock = new Moq.Mock<IDbConnectionConfigurator>();
            mock.Setup(tt => tt.ConnectionStringExists()).ReturnsAsync(false);
            mock.Setup(tt => tt.LoadConnectionString()).ReturnsAsync(string.Empty);
            var factory = new DefaultDbConnectionFactory();

            var sqlConn = factory.Create(mock.Object).GetAwaiter().GetResult();
            sqlConn.ShouldNotBeNull();
            sqlConn.ConnectionString.ShouldBe(string.Empty);
        }

        [Fact]
        public void Should_Return_SqlConnection_With_Given_Connection_String()
        {
            const string connString =
                "Server=myServerName\\myInstanceName;Database=myDataBase;User Id=myUsername;Password = myPassword;";
            var mock = new Moq.Mock<IDbConnectionConfigurator>();
            mock.Setup(tt => tt.ConnectionStringExists()).ReturnsAsync(false);
            mock.Setup(tt => tt.LoadConnectionString()).ReturnsAsync(connString);
            var factory = new DefaultDbConnectionFactory();

            var sqlConn = factory.Create(mock.Object).GetAwaiter().GetResult();
            sqlConn.ShouldNotBeNull();
            sqlConn.ConnectionString.ShouldBe(connString);
        }
    }
}