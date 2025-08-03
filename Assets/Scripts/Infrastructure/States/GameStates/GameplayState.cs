using Cysharp.Threading.Tasks;
using Gameplay.Core.Grids;
using Infrastructure.Services.Loading;
using Infrastructure.Services.SceneLocator;
using Infrastructure.Services.SceneTransition;

namespace Infrastructure.States.GameStates
{
    public class GameplayState : IState
    {
        private readonly ISceneTransitionService _sceneTransition;
        private readonly ISceneServiceLocator _serviceLocator;

        private GridSystem _gridSystem;

        public GameplayState(
            ISceneTransitionService sceneTransition, 
            ISceneServiceLocator serviceLocator)
        {
            _sceneTransition = sceneTransition;
            _serviceLocator = serviceLocator;
        }

        public async void Enter()
        {
            await _sceneTransition.TransitionAsync(SceneType.Gameplay, InitializeGameplayAsync);
        }

        public void Exit()
        {
        }

        private async UniTask InitializeGameplayAsync()
        {
            _gridSystem = _serviceLocator.Get<GridSystem>();
            _gridSystem.Initialize();
            _gridSystem.GridMover.StartMove();
            
            await UniTask.CompletedTask;
        }
    }
}