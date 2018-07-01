using System;

namespace DatabaseImporter.Common.Infastructure
{
    public interface IServiceLocator
    {
        #region Typed Services
        void AddService<TIService, TService>(ServiceType serviceType) where TIService : class where TService :  TIService, new();

        void AddService<TService>(ServiceType serviceType) where TService :  new();

        void AddService<TIService>(ServiceType serviceType, Func<IServiceLocator, TIService> factory) where TIService : class;

        TIService GetService<TIService>();

        object GetService(Type type);
        #endregion
        #region Keyed Services

        void AddService<TIService, TService>(string key, ServiceType serviceType) where TIService : class where TService :  TIService, new();

        void AddService<TService>(string key, ServiceType serviceType) where TService : new();

        void AddService<TIService>(string key, ServiceType serviceType, Func<IServiceLocator, TIService> factory) where TIService : class;

        TIService GetService<TIService>(string key);

        object GetService(string key);
        #endregion

    }
}