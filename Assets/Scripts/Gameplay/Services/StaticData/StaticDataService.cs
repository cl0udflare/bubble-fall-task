using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Ball.StaticData;
using Gameplay.Core.Board.StaticData;
using Gameplay.Core.Levels.StaticData;
using Infrastructure.Services.AssetManagement;
using Logging;
using UI.Window;
using UI.Window.StaticData;

namespace Gameplay.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WINDOW_PATH = "StaticData/UI/WindowConfig";
        private const string LEVEL_PATH = "StaticData/Gameplay/LevelConfig";
        private const string BOARD_PATH = "StaticData/Gameplay/BoardConfig";
        private const string BALL_PATH = "StaticData/Gameplay/BallConfig";

        private readonly ILoaderService _loaderService;
        
        private Dictionary<WindowType, WindowConfigData> _windowConfigs;
        
        public LevelConfig LevelConfig { get; private set; }
        public BoardConfig BoardConfig {get; private set;}
        public BallConfig BallConfig { get; private set; }

        public StaticDataService(ILoaderService loaderService) => 
            _loaderService = loaderService;

        public async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            await LoadAll();
            DebugLogger.LogMessage(message: $"Loaded", sender: this);
        }

        public WindowConfigData GetWindow(WindowType windowType) => _windowConfigs.GetValueOrDefault(windowType);

        private async UniTask LoadAll()
        {
            await InitializeWindowConfig();
            await InitializeLevelConfig();
            await InitializeBoardConfig();
            await InitializeBallConfig();
        }

        private async UniTask InitializeWindowConfig()
        {
            WindowConfig windowConfig = await _loaderService.LoadAsset<WindowConfig>(WINDOW_PATH);
            _windowConfigs = windowConfig.Windows.ToDictionary(w => w.Type, w => w);
        }

        private async UniTask InitializeLevelConfig() => 
            LevelConfig = await _loaderService.LoadAsset<LevelConfig>(LEVEL_PATH);

        private async UniTask InitializeBoardConfig() => 
            BoardConfig = await _loaderService.LoadAsset<BoardConfig>(BOARD_PATH);
        
        private async UniTask InitializeBallConfig() => 
            BallConfig = await _loaderService.LoadAsset<BallConfig>(BALL_PATH);
    }
}