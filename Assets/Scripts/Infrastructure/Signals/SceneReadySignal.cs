using System.Collections.Generic;

namespace Infrastructure.Signals
{
    public class SceneReadySignal
    {
        private Dictionary<System.Type, object> Services { get; }

        public SceneReadySignal(Dictionary<System.Type, object> services) => 
            Services = services;

        public T GetService<T>() where T : class => 
            Services[typeof(T)] as T;
    }
}