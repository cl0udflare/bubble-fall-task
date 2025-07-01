using System.Collections.Generic;
using Gameplay.Core.Ball;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Ball.Factory;
using Gameplay.Core.Levels.SpawnStrategies;
using Gameplay.Core.Ball.StaticData;
using Gameplay.Core.Board;
using Gameplay.Services.Randoms;
using Logging;
using UnityEngine;

namespace Gameplay.Core.Levels.Builder
{
    public class LevelBuilder
    {
        private readonly IBallFactory _ballFactory;
        private readonly IRandomService _randomService;

        private BallConfig _ballConfig;
        private IBallSpawnStrategy _spawnStrategy;
        private IBoardSystem _boardSystem;

        public LevelBuilder(IBallFactory ballFactory, IRandomService randomService)
        {
            _ballFactory = ballFactory;
            _randomService = randomService;
        }

        public void Initialize(BallConfig ballConfig, IBoardSystem boardSystem)
        {
            _ballConfig = ballConfig;
            _boardSystem = boardSystem;
            
            _spawnStrategy = new WaveBallSpawnStrategy(_randomService);
        }

        public void Build()
        {
            IEnumerable<Vector2Int> layout = _boardSystem.GetAllCells();
            List<BallData> ballsToSpawn = _spawnStrategy.GenerateBalls(layout);

            foreach (BallData ballData in ballsToSpawn)
            {
                if (_boardSystem.TryAddBall(ballData.GridPosition, ballData.Color))
                {
                    Vector3 worldPosition = _boardSystem.GridToWorld(ballData.GridPosition);
                    _ballFactory.CreateBall(_ballConfig, worldPosition, ballData.Color);
                }
                else
                {
                    DebugLogger.LogWarning($"Failed to add ball at {ballData.GridPosition}");
                }
            }
        }
    }
}