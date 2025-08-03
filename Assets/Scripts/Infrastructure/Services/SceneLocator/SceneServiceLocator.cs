using System;
using System.Collections.Generic;

namespace Infrastructure.Services.SceneLocator
{
    public class SceneServiceLocator : ISceneServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();

        public T Get<T>() where T : class
        {
            _services.TryGetValue(typeof(T), out var service);
            return service as T;
        }

        public void Register(object service)
        {
            Type type = service.GetType();
            _services[type] = service;
        }

        public void Clear() => _services.Clear();
    }
}