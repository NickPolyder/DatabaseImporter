using System;

namespace DatabaseImporter.Common.Infastructure
{
    public interface IServiceLocator
    {
        void AddService<TIService, TService>(ServiceType serviceType) where TIService : class where TService : class, new();

        void AddService<TService>(ServiceType serviceType) where TService : class, new();

        void AddService<TIService>(ServiceType serviceType, Func<IServiceLocator, TIService> factory) where TIService : class;

        void AddService<TIService,TService>(ServiceType serviceType, Func<IServiceLocator, TService> factory) where TIService : class where TService : class;

        TIService GetService<TIService>() where TIService : class;

        object GetService(Type type);
    }
}