using Cysharp.Threading.Tasks;
using Gameplay.Services.StaticData;
using UI.Window.StaticData;
using UnityEngine;
using Zenject;

namespace UI.Window.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;
        
        private Transform _windowRoot;

        public WindowFactory(IStaticDataService staticDataService, DiContainer container)
        {
            _staticDataService = staticDataService;
            _container = container;
        }

        public async UniTask<WindowBase> CreateWindowByType(WindowType type) => 
            await CreateWindow(type);
        
        public void SetRoot(Transform root) => 
            _windowRoot = root;

        private async UniTask<WindowBase> CreateWindow(WindowType type)
        {
            WindowStaticData windowData = _staticDataService.GetWindow(type);
            
            WindowBase[] instance = await Object.InstantiateAsync(windowData.Prefab, _windowRoot).ToUniTask();
            WindowBase window = instance[0];
            
            _container.InjectGameObject(window.gameObject);
            return window;
        }
    }
}