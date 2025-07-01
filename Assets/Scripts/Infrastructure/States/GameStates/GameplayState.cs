using Cysharp.Threading.Tasks;
using Gameplay.Core.Ball;
using Gameplay.Core.Ball.StaticData;
using Gameplay.Core.Board;
using Gameplay.Core.Board.StaticData;
using Gameplay.Core.Levels;
using Gameplay.Core.Levels.Builder;
using Gameplay.Core.Levels.SpawnStrategies;
using Gameplay.Core.Levels.StaticData;
using Gameplay.Services.StaticData;
using Gameplay.Services.Systems;
using Infrastructure.Services.Loading;

namespace Infrastructure.States.GameStates
{
    public class GameplayState : IState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticData;
        private readonly ISystemFactory _systemFactory;

        public GameplayState(
            ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticData, 
            ISystemFactory systemFactory)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticData = staticData;
            _systemFactory = systemFactory;
        }

        public async void Enter() => 
            await _sceneLoaderService.LoadSceneAsync(SceneType.Gameplay, postLoadLogic: LoadCoreDependencies);

        public void Exit() { }

        private UniTask LoadCoreDependencies()
        {
            InitializeGameplay();
            
            return UniTask.CompletedTask;
        }

        private void InitializeGameplay()
        {
            LevelConfig levelConfig = _staticData.LevelConfig;
            
            IBoardSystem boardSystem = CreateBoard(levelConfig.BoardConfig);
            CreateLevelBuilder(boardSystem);
        }

        private IBoardSystem CreateBoard(BoardConfig boardConfig)
        {
            BoardSystem board = _systemFactory.Create<BoardSystem>();
            board.Initialize(boardConfig);
            
            return board;
        }

        private LevelBuilder CreateLevelBuilder(IBoardSystem boardSystem)
        {
            BallConfig ballConfig = _staticData.BallConfig;
            IBallSpawnStrategy spawnStrategy = _systemFactory.Create<WaveBallSpawnStrategy>();
            LevelBuilder builder = _systemFactory.Create<LevelBuilder>();
           
            builder.Initialize(ballConfig, boardSystem, spawnStrategy);
            builder.Build();
            
            return builder;
        }
    }
}