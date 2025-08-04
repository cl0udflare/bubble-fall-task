using System.Collections.Generic;
using Gameplay.Core.Balls;
using Gameplay.Core.Balls.Data;
using Gameplay.Core.Balls.Factory;
using Gameplay.Core.Balls.StaticData;
using Gameplay.Core.Grids;
using Gameplay.Core.Grids.Data;
using Gameplay.Core.Levels.Data;
using Gameplay.Core.Levels.SpawnStrategies;
using Logging;
using UnityEngine;
using Zenject;

namespace Gameplay.Core.Levels.Builder
{
    public class LevelBuilder : MonoBehaviour
    {
        private IBallFactory _ballFactory;
        private BallConfig _ballConfig;
        private GridSystem _gridSystem;
        private BallSpawnStrategyManager _strategyManager;
        private GridMover _gridMover;

        [Inject]
        public void Construct(IBallFactory ballFactory, GridSystem gridSystem, GridMover gridMover)
        {
            _ballFactory = ballFactory;
            _gridSystem = gridSystem;
            _gridMover = gridMover;
        }

        private void OnDestroy()
        {
            if (_gridSystem != null)
                _gridSystem.OnCellAdded -= SpawnNewBalls;

            _strategyManager?.Dispose();
        }

        public void Initialize(BallConfig ballConfig, BallSpawnType spawnType)
        {
            _ballConfig = ballConfig;

            _ballFactory.SetRoot(_gridSystem.transform);
            _gridSystem.OnCellAdded += SpawnNewBalls;

            _strategyManager = new BallSpawnStrategyManager(_gridMover);
            _strategyManager.Initialize(spawnType, _gridSystem.Config.GridSetup.CellSize);
        }

        public void Build()
        {
            List<BallData> ballsToSpawn = _strategyManager.GenerateBalls(GetCellsPositions());

            foreach (BallData ballData in ballsToSpawn)
                SpawnBallAtPosition(ballData.GridPosition, ballData.Color);
        }

        private void SpawnBallAtPosition(Vector2Int gridPosition, BallColor color)
        {
            if (_gridSystem.TryAddCellData(gridPosition, out GridData boardData))
            {
                Vector3 worldPosition = _gridSystem.CellToWorld(gridPosition);
                BallView ball = _ballFactory.Create(_ballConfig, worldPosition, color);

                boardData.View = ball;
                boardData.Color = color;
            }
            else
            {
                DebugLogger.LogWarning($"Failed to add ball at {gridPosition}");
            }
        }

        private void SpawnNewBalls(List<Vector2Int> cells)
        {
            foreach (Vector2Int cell in cells)
            {
                BallColor color = _strategyManager.GetBallColorForPosition(_gridSystem.CellToWorld(cell));
                SpawnBallAtPosition(cell, color);
            }
        }

        private Dictionary<Vector2Int, Vector3> GetCellsPositions()
        {
            Dictionary<Vector2Int, Vector3> positions = new Dictionary<Vector2Int, Vector3>();
            foreach (KeyValuePair<Vector2Int, GridData> cell in _gridSystem.GetCells())
                positions.Add(cell.Key, _gridSystem.CellToWorld(cell.Key));

            return positions;
        }
    }
}