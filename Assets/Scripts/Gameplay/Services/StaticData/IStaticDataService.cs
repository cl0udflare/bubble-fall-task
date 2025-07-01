using Gameplay.Core.Levels.StaticData;
using Infrastructure.Bootstrapper;
using UI.Window;
using UI.Window.StaticData;

namespace Gameplay.Services.StaticData
{
    public interface IStaticDataService : IInitializableAsync
    {
        WindowStaticData GetWindow(WindowType windowType);
        LevelConfig GetLevel();
    }
}