using System;
using System.Collections.Generic;

namespace DatabaseImporter.Common.Infastructure
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<string, ServiceItem> _keyServices = new Dictionary<string, ServiceItem>();
        private readonly Dictionary<Type, ServiceItem> _services = new Dictionary<Type, ServiceItem>();

        #region Typed Services

        public void AddService<TIService, TService>(ServiceType serviceType) where TIService : class where TService : TIService, new()
        {
            var typeService = typeof(TIService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name} already exists.");

            _services.Add(typeService, GetLocator(serviceType, (__) => new TService()));
        }

        public void AddService<TService>(ServiceType serviceType) where TService : new()
        {
            var typeService = typeof(TService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name} already exists.");

            _services.Add(typeService, GetLocator(serviceType, (__) => new TService()));
        }

        public void AddService<TIService>(ServiceType serviceType, Func<IServiceLocator, TIService> factory) where TIService : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            var typeService = typeof(TIService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name}already exists.");

            _services.Add(typeService, GetLocator(serviceType, factory));
        }

        public TIService GetService<TIService>()
        {
            var typeService = typeof(TIService);
            if (!_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name} does not exist.");

            return _services[typeService].GetInstance<TIService>();
        }

        public object GetService(Type type)
        {
            if (!_services.ContainsKey(type))
                throw new ArgumentException($"The service: {type.Name} does not exist.");

            return _services[type].GetInstance<object>();
        }

        #endregion

        #region Keyed Services

        public void AddService<TIService, TService>(string key, ServiceType serviceType)
            where TIService : class where TService : TIService, new()
        {
            KeyGuard(key);
            _keyServices.Add(key, GetLocator(serviceType, (__) => new TService()));
        }

        public void AddService<TService>(string key, ServiceType serviceType) where TService : new()
        {
            KeyGuard(key);
            _keyServices.Add(key, GetLocator(serviceType, (__) => new TService()));
        }

        public void AddService<TIService>(string key, ServiceType serviceType, Func<IServiceLocator, TIService> factory) where TIService : class
        {
            KeyGuard(key);
            _keyServices.Add(key, GetLocator(serviceType, factory));
        }
        
        public TIService GetService<TIService>(string key)
        {
            KeyNullGuard(key);

            if (!_keyServices.ContainsKey(key))
                throw new ArgumentException("Key does not exist.");

            return _keyServices[key].GetInstance<TIService>();
        }

        public object GetService(string key)
        {
            KeyNullGuard(key);

            if (!_keyServices.ContainsKey(key))
                throw new ArgumentException("Key does not exist.");

            return _keyServices[key].GetInstance<object>();
        }

        #endregion

        #region Helpers

        private void KeyGuard(string key)
        {
            KeyNullGuard(key);

            if (_keyServices.ContainsKey(key))
            {
                throw new ArgumentException("Key already exists.");
            }
        }

        private static void KeyNullGuard(string key)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        private ServiceItem GetLocator(ServiceType type, Func<IServiceLocator, object> factoryFunc)
        {
            switch (type)
            {
                case ServiceType.Singleton:
                    return new SingletonServiceItem(this, factoryFunc);
                case ServiceType.Transient:
                default:
                    return new TransientServiceItem(this, factoryFunc);
            }
        }

        #endregion

        #region ServiceItem

        private abstract class ServiceItem
        {
            protected readonly IServiceLocator Parent;

            protected ServiceItem(IServiceLocator parent)
            {
                Parent = parent;
            }
            public abstract TService GetInstance<TService>();
        }

        private class SingletonServiceItem : ServiceItem
        {
            private readonly Lazy<object> _objInstance;

            public SingletonServiceItem(IServiceLocator parent, Func<IServiceLocator, object> factory) : base(parent)
            {
                if (factory == null) throw new ArgumentNullException(nameof(factory));
                _objInstance = new Lazy<object>(() => factory(Parent));
            }

            public override TService GetInstance<TService>()
            {
                return (TService)_objInstance.Value;
            }
        }

        private class TransientServiceItem : ServiceItem
        {
            private readonly Func<IServiceLocator, object> _factory;

            public TransientServiceItem(IServiceLocator parent, Func<IServiceLocator, object> factory) : base(parent)
            {
                if (factory == null) throw new ArgumentNullException(nameof(factory));
                _factory = factory;
            }

            public override TService GetInstance<TService>()
            {
                return (TService)_factory?.Invoke(Parent);
            }
        }

        #endregion

    }
}