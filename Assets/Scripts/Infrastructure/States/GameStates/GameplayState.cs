using Cysharp.Threading.Tasks;
using Gameplay.Services.StaticData;
using Infrastructure.Services.Loading;
namespace Infrastructure.States.GameStates
{
    public class GameplayState : IState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticData;

        public GameplayState(ISceneLoaderService sceneLoaderService, IStaticDataService staticData)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticData = staticData;
        }

        public async void Enter() => 
            await _sceneLoaderService.LoadSceneAsync(SceneType.None, postLoadLogic: LoadCoreDependencies);

        public void Exit() { }

        private async UniTask LoadCoreDependencies()
        {
            
        }
    }
}