using Gameplay.Core.Grids;
using Gameplay.Services.StaticData;
using Infrastructure.Services.Loading;
using Infrastructure.Signals;
using Zenject;

namespace Infrastructure.States.GameStates
{
    public class GameplayState : IState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticData;
        private readonly SignalBus _signalBus;
        
        private GridSystem _gridSystem;

        public GameplayState(
            ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticData, 
            SignalBus signalBus)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticData = staticData;
            _signalBus = signalBus;
        }

        public async void Enter()
        {
            _signalBus.Subscribe<SceneReadySignal>(OnSceneReady);
            await _sceneLoaderService.LoadSceneAsync(SceneType.Gameplay);
        }

        public void Exit()
        {
        }

        private void OnSceneReady(SceneReadySignal signal)
        {
            _signalBus.Unsubscribe<SceneReadySignal>(OnSceneReady);

            _gridSystem = signal.GetService<GridSystem>();

            OnLoaded();
        }

        private void OnLoaded()
        {
            InitializeGameplay();
        }

        private void InitializeGameplay()
        {
            _gridSystem.Initialize();
            _gridSystem.GridMover.StartMove();
            
            // CreateLevelBuilder();

            // Tester();
        }

        // private void Tester()
        // {
        //     BallRemoverTester BallRemoverTester = Object.FindFirstObjectByType<BallRemoverTester>();
        //     BallRemoverTester.CreatePhysicsSystem(_boardSystem);
        // } 

        // private ILevelBuilder CreateLevelBuilder()
        // {
        //     BallConfig ballConfig = _staticData.BallConfig;
        //     ILevelBuilder builder = _systemFactory.Create<LevelBuilder>();
        //    
        //     builder.Initialize(ballConfig, _boardSystem);
        //     builder.Build();
        //     
        //     return builder;
        // }
    }
}