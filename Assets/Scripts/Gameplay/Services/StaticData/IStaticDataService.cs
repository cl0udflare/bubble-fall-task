using Gameplay.Core.Ball.StaticData;
using Gameplay.Core.Board.StaticData;
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
        BoardConfig BoardConfig { get; }
        BallConfig BallConfig { get; }
    }
}