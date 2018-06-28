using System;

namespace DatabaseImporter.Common.Infastructure
{
    public static class ServiceLocatorExtensions
    {
        #region Transient
        public static void AddTransient<TIService, TService>(this IServiceLocator locator)
            where TIService : class where TService : class, new()
        {
            locator.AddService<TIService, TService>(ServiceType.Transient);
        }

        public static void AddTransient<TService>(this IServiceLocator locator) where TService : class, new()
        {
            locator.AddService<TService>(ServiceType.Transient);
        }

        public static void AddTransient<TIService>(this IServiceLocator locator, Func<IServiceLocator, TIService> factory)
            where TIService : class
        {
            locator.AddService(ServiceType.Transient, factory);
        }

        public static void AddTransient<TIService, TService>(this IServiceLocator locator, Func<IServiceLocator, TService> factory)
            where TIService : class where TService : class
        {
            locator.AddService<TIService, TService>(ServiceType.Transient, factory);
        }
        #endregion

        #region Singleton
        public static void AddSingleton<TIService, TService>(this IServiceLocator locator)
            where TIService : class where TService : class, TIService, new()
        {
            locator.AddService<TIService, TService>(ServiceType.Singleton);
        }

        public static void AddSingleton<TService>(this IServiceLocator locator) where TService : class, new()
        {
            locator.AddService<TService>(ServiceType.Singleton);
        }

        public static void AddSingleton<TIService>(this IServiceLocator locator, Func<IServiceLocator, TIService> factory)
            where TIService : class
        {
            locator.AddService(ServiceType.Singleton, factory);
        }

        public static void AddSingleton<TIService, TService>(this IServiceLocator locator, Func<IServiceLocator, TService> factory)
            where TIService : class where TService : class
        {
            locator.AddService<TIService, TService>(ServiceType.Singleton, factory);
        }
        #endregion

        public static object GetServiceOrDefault(this IServiceLocator locator, Type type)
        {
            try
            {
                return locator.GetService(type);
            }
            catch
            {
                return null;
            }
        }

        public static TService GetServiceOrDefault<TService>(this IServiceLocator locator) where TService : class
        {
            try
            {
                return locator.GetService<TService>();
            }
            catch
            {
                return default(TService);
            }
        }
    }
}