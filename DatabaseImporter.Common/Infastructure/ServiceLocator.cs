using System;
using System.Collections.Generic;

namespace DatabaseImporter.Common.Infastructure
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, ServiceItem> _services = new Dictionary<Type, ServiceItem>();

        public void AddService<TIService, TService>(ServiceType serviceType) where TIService : class where TService : class, new()
        {
            var typeService = typeof(TIService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name} already exists.");

            _services.Add(typeService, GetLocator(serviceType, (__) => new TService()));
        }

        public void AddService<TService>(ServiceType serviceType) where TService : class, new()
        {
            var typeService = typeof(TService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name} already exists.");

            _services.Add(typeService, GetLocator(serviceType, (__) => new TService()));
        }

        public void AddService<TIService>(ServiceType serviceType,Func<IServiceLocator, TIService> factory) where TIService : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            var typeService = typeof(TIService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name}already exists.");

            _services.Add(typeService, GetLocator(serviceType, (__) => factory.Invoke(this)));
        }

        public void AddService<TIService, TService>(ServiceType serviceType, Func<IServiceLocator, TService> factory) where TIService : class where TService : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            var typeService = typeof(TIService);
            if (_services.ContainsKey(typeService))
                throw new ArgumentException($"The service: {typeService.Name}already exists.");

            _services.Add(typeService, GetLocator(serviceType, (__) => factory.Invoke(this)));
        }


        public TIService GetService<TIService>() where TIService : class
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

        private abstract class ServiceItem
        {
            protected readonly IServiceLocator Parent;

            protected ServiceItem(IServiceLocator parent)
            {
                Parent = parent;
            }
            public abstract TService GetInstance<TService>() where TService : class;
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
    }
}