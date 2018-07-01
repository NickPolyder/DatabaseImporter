using System;
using DatabaseImporter.Common.Infastructure;
using DatabaseImporter.Common.Tests.TestModels;
using Shouldly;
using Xunit;

namespace DatabaseImporter.Common.Tests
{
    public class ServiceLocatorTests
    {
        #region Typed Services
        [Fact]
        public void Register_Single_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<GuidTest>(ServiceType.Transient);

            var guid1 = serviceLocator.GetService<GuidTest>().Guid;
            var guid2 = serviceLocator.GetService<GuidTest>().Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_Single_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<GuidTest>(ServiceType.Singleton);

            var guid1 = serviceLocator.GetService<GuidTest>().Guid;
            var guid2 = serviceLocator.GetService<GuidTest>().Guid;
            guid1.ShouldBe(guid2);
        }

        [Fact]
        public void Register_With_Interface_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest, GuidTest>(ServiceType.Transient);

            var guid1 = serviceLocator.GetService<IGuidTest>().Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>().Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_With_Interface_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest, GuidTest>(ServiceType.Singleton);

            var guid1 = serviceLocator.GetService<IGuidTest>().Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>().Guid;
            guid1.ShouldBe(guid2);
        }

        [Fact]
        public void Register_Factory_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService(ServiceType.Transient, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<GuidTest>().Guid;
            var guid2 = serviceLocator.GetService<GuidTest>().Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_Factory_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService(ServiceType.Singleton, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<GuidTest>().Guid;
            var guid2 = serviceLocator.GetService<GuidTest>().Guid;
            guid1.ShouldBe(guid2);
        }

        [Fact]
        public void Register_With_Interface_Factory_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest>(ServiceType.Transient, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<IGuidTest>().Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>().Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_With_Interface_Factory_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest>(ServiceType.Singleton, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<IGuidTest>().Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>().Guid;
            guid1.ShouldBe(guid2);
        }
        #endregion


        #region Keyed Services
        [Fact]
        public void Register_Keyed_Single_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<GuidTest>("key", ServiceType.Transient);

            var guid1 = serviceLocator.GetService<GuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<GuidTest>("key").Guid;

            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_Keyed_Single_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<GuidTest>("key", ServiceType.Singleton);

            var guid1 = serviceLocator.GetService<GuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<GuidTest>("key").Guid;
            guid1.ShouldBe(guid2);
        }

        [Fact]
        public void Register_Keyed_With_Interface_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest, GuidTest>("key", ServiceType.Transient);

            var guid1 = serviceLocator.GetService<IGuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>("key").Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_Keyed_With_Interface_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest, GuidTest>("key", ServiceType.Singleton);

            var guid1 = serviceLocator.GetService<IGuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>("key").Guid;
            guid1.ShouldBe(guid2);
        }

        [Fact]
        public void Register_Keyed_Factory_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService("key", ServiceType.Transient, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<GuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<GuidTest>("key").Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_Keyed_Factory_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService("key", ServiceType.Singleton, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<GuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<GuidTest>("key").Guid;
            guid1.ShouldBe(guid2);
        }

        [Fact]
        public void Register_Keyed_With_Interface_Factory_Transient_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest>("key", ServiceType.Transient, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<IGuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>("key").Guid;
            guid1.ShouldNotBe(guid2);
        }

        [Fact]
        public void Register_Keyed_With_Interface_Factory_Singleton_Service()
        {
            var serviceLocator = new ServiceLocator();

            serviceLocator.AddService<IGuidTest>("key", ServiceType.Singleton, (__) => new GuidTest());

            var guid1 = serviceLocator.GetService<IGuidTest>("key").Guid;
            var guid2 = serviceLocator.GetService<IGuidTest>("key").Guid;
            guid1.ShouldBe(guid2);
        }
        #endregion
    }

}