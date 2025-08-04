using Cysharp.Threading.Tasks;
using Gameplay.Services.StaticData;
using UI.Windows.StaticData;
using UnityEngine;

namespace UI.Windows.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IStaticDataService _staticDataService;
        
        private Transform _windowRoot;

        public WindowFactory(IStaticDataService staticDataService) => 
            _staticDataService = staticDataService;

        public async UniTask<WindowBase> CreateWindowByType(WindowType type) => 
            await CreateWindow(type);
        
        public void SetRoot(Transform root) => 
            _windowRoot = root;

        private async UniTask<WindowBase> CreateWindow(WindowType type)
        {
            WindowConfigData windowData = _staticDataService.GetWindow(type);

            WindowBase[] windows = await Object.InstantiateAsync(windowData.Prefab, parent: _windowRoot);
            WindowBase window = windows[0];
            
            return window;
        }
    }
}