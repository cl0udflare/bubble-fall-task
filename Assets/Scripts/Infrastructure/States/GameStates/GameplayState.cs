using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Grids;
using Gameplay.Core.Levels.Builder;
using Gameplay.Services.StaticData;
using Infrastructure.Services.Loading;
using Infrastructure.Services.SceneTransition;
using Zenject;

namespace Infrastructure.States.GameStates
{
    public class GameplayState : IState
    {
        private readonly ISceneTransitionService _sceneTransition;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;

        private GridSystem _gridSystem;
        private LevelBuilder _levelBuilder;
        private GridMover _gridMover;

        public GameplayState(
            ISceneTransitionService sceneTransition,
            IStaticDataService staticData,
            DiContainer container)
        {
            _sceneTransition = sceneTransition;
            _staticData = staticData;
            _container = container;
        }

        public async void Enter()
        {
            await _sceneTransition.TransitionAsync(SceneType.Gameplay, async () =>
            {
                await InitializeGameplayAsync();
                StartGameplay();
            });
        }

        public void Exit()
        {
            _gridMover.OnCellShifted -= _gridSystem.AddNewRows;
        }

        private async UniTask InitializeGameplayAsync()
        { 
            SceneContext sceneContext = _container.Resolve<SceneContextRegistry>().SceneContexts.First();
            
            _gridSystem = sceneContext.Container.Resolve<GridSystem>();
            _gridSystem.Initialize(_staticData.LevelConfig.GridConfig);
            
            _gridMover = sceneContext.Container.Resolve<GridMover>();
            _gridMover.Initialize(_gridSystem.Config.MovementSetup, _gridSystem.Config.GridSetup.CellSize);
            _gridMover.OnCellShifted += _gridSystem.AddNewRows;
            
            _levelBuilder = sceneContext.Container.Resolve<LevelBuilder>();
            _levelBuilder.Initialize(_staticData.BallConfig, _staticData.LevelConfig.SpawnSetup.SpawnType); 
            
            await UniTask.CompletedTask;
        }

        private void StartGameplay()
        {
            _levelBuilder.Build();
            _gridMover.StartMove();
        }
    }
}