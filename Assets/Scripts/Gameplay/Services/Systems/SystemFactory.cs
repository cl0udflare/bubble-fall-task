using Zenject;

namespace Gameplay.Services.Systems
{
    public class SystemFactory : ISystemFactory
    {
        private readonly DiContainer _container;

        public SystemFactory(DiContainer container) => 
            _container = container;

        public T Create<T>() where T : class =>
            _container.Instantiate<T>();

        public T Create<T>(params object[] args) where T : class =>
            _container.Instantiate<T>(args);
    }
}