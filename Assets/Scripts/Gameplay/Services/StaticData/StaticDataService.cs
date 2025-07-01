using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        private readonly ILoaderService _loaderService;
        
        private Dictionary<WindowType, WindowStaticData> _windowConfigs;
        private LevelConfig _levelConfig;

        public StaticDataService(ILoaderService loaderService) => 
            _loaderService = loaderService;

        public async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            await LoadAll();
            DebugLogger.LogMessage(message: $"Loaded", sender: this);
        }

        public WindowStaticData GetWindow(WindowType windowType) => _windowConfigs.GetValueOrDefault(windowType);
        public LevelConfig GetLevel() => _levelConfig;

        private async UniTask LoadAll()
        {
            await InitializeWindowConfig();
            await InitializeLevelConfig();
        }

        private async UniTask InitializeWindowConfig()
        {
            WindowConfig windowConfig = await _loaderService.LoadAsset<WindowConfig>(WINDOW_PATH);
            _windowConfigs = windowConfig.Windows.ToDictionary(w => w.Type, w => w);
        }

        private async UniTask InitializeLevelConfig()
        {
            LevelConfig levelConfig = await _loaderService.LoadAsset<LevelConfig>(LEVEL_PATH);
            _levelConfig = levelConfig;
        }
    }
}