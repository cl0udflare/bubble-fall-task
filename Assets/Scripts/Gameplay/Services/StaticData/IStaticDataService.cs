using Gameplay.Core.Balls.StaticData;
using Gameplay.Core.Levels.StaticData;
using Infrastructure.Bootstrapper;
using UI.Window;
using UI.Window.StaticData;

namespace Gameplay.Services.StaticData
{
    public interface IStaticDataService : IInitializableAsync
    {
        WindowConfigData GetWindow(WindowType windowType);
        LevelConfig LevelConfig { get; }
        BallConfig BallConfig { get; }
    }
}