using System.Collections.Generic;
using Gameplay.Core.Balls;
using Gameplay.Core.Balls.Data;
using Gameplay.Core.Balls.Factory;
using Gameplay.Core.Balls.StaticData;
using Gameplay.Core.Grids;
using Gameplay.Core.Grids.Data;
using Gameplay.Core.Levels.SpawnStrategies;
using Gameplay.Services.UnityRandom;
using Logging;
using UnityEngine;

namespace Gameplay.Core.Levels.Builder
{
    public class LevelBuilder : ILevelBuilder
    {
        private readonly IBallFactory _ballFactory;
        private readonly IRandomService _randomService;

        private BallConfig _ballConfig;
        private IBallSpawnStrategy _spawnStrategy;
        private GridSystem _gridSystem;

        public LevelBuilder(IBallFactory ballFactory, IRandomService randomService)
        {
            _ballFactory = ballFactory;
            _randomService = randomService;
        }

        public void Initialize(BallConfig ballConfig, GridSystem gridSystem)
        {
            _ballConfig = ballConfig;
            _gridSystem = gridSystem;
            
            _spawnStrategy = new WaveBallSpawnStrategy(_randomService);
        }

        public void Build()
        {
            IEnumerable<Vector2Int> layout = _gridSystem.GetCells().Keys;
            List<BallData> ballsToSpawn = _spawnStrategy.GenerateBalls(layout);

            foreach (BallData ballData in ballsToSpawn)
            {
                if (_gridSystem.TryAddCellData(ballData.GridPosition, out GridData boardData))
                {
                    Vector3 worldPosition = _gridSystem.CellToWorld(ballData.GridPosition);
                    BallView ball = _ballFactory.CreateBall(_ballConfig, worldPosition, ballData.Color);
                    
                    boardData.View = ball;
                    boardData.Color = ballData.Color;
                }
                else
                {
                    DebugLogger.LogWarning($"Failed to add ball at {ballData.GridPosition}");
                }
            }
        }
    }
}