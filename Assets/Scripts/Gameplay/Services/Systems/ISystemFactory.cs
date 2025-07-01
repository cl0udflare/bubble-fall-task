namespace Gameplay.Services.Systems
{
    public interface ISystemFactory
    {
        T Create<T>() where T : class;
        T Create<T>(params object[] args) where T : class;
    }
}