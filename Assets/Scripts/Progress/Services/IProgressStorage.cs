using Infrastructure.Bootstrapper;

namespace Progress.Services
{
    public interface IProgressStorage : IInitializableAsync
    {
        void Save();
        void Load();
        void Reset();
    }
}