using Cysharp.Threading.Tasks;
using Gameplay.Core.Ball.Debugger;
using Gameplay.Core.Ball.StaticData;
using Gameplay.Core.Board;
using Gameplay.Core.Board.StaticData;
using Gameplay.Core.Levels.Builder;
using Gameplay.Core.Levels.StaticData;
using Gameplay.Services.StaticData;
using Gameplay.Services.Systems;
using Infrastructure.Services.Loading;
using UnityEngine;

namespace Infrastructure.States.GameStates
{
    public class GameplayState : IState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticData;
        private readonly ISystemFactory _systemFactory;
        
        private IBoardSystem _boardSystem;

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

        public void Exit()
        {
            _boardSystem.Dispose();
        }

        private UniTask LoadCoreDependencies()
        {
            InitializeGameplay();
            
            return UniTask.CompletedTask;
        }

        private void InitializeGameplay()
        {
            LevelConfig levelConfig = _staticData.LevelConfig;
            
            _boardSystem = CreateBoard(levelConfig.BoardConfig);
            CreateLevelBuilder();

            Tester();
        }

        private void Tester()
        {
            BallRemoverTester BallRemoverTester = Object.FindFirstObjectByType<BallRemoverTester>();
            BallRemoverTester.CreatePhysicsSystem(_boardSystem);
        } 

        private IBoardSystem CreateBoard(BoardConfig boardConfig)
        {
            BoardSystem board = _systemFactory.Create<BoardSystem>();
            board.Initialize(boardConfig);
            return board;
        }

        private ILevelBuilder CreateLevelBuilder()
        {
            BallConfig ballConfig = _staticData.BallConfig;
            ILevelBuilder builder = _systemFactory.Create<LevelBuilder>();
           
            builder.Initialize(ballConfig, _boardSystem);
            builder.Build();
            
            return builder;
        }
    }
}