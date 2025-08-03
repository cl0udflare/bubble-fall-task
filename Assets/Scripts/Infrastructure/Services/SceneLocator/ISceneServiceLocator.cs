namespace Infrastructure.Services.SceneLocator
{
    public interface ISceneServiceLocator
    {
        T Get<T>() where T : class;
        void Register(object service);
        void Clear();
    }
}