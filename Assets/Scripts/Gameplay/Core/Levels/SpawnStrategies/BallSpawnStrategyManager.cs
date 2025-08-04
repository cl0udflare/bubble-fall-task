using System.Collections.Generic;
using Gameplay.Core.Balls.Data;
using Gameplay.Core.Grids;
using Gameplay.Core.Levels.Data;
using UnityEngine;

namespace Gameplay.Core.Levels.SpawnStrategies
{
    public class BallSpawnStrategyManager
    {
        private readonly GridMover _gridMover;
        
        private IBallSpawnStrategy _spawnStrategy;

        public BallSpawnStrategyManager(GridMover gridMover) => 
            _gridMover = gridMover;

        public void Initialize(BallSpawnType spawnType, float cellSize)
        {
            _spawnStrategy = new ConcentricFlowStrategy(cellSize);

            _gridMover.OnCellShifted += _spawnStrategy.ShiftPattern;
        }

        public void Dispose() => 
            _gridMover.OnCellShifted -= _spawnStrategy.ShiftPattern;

        public List<BallData> GenerateBalls(Dictionary<Vector2Int, Vector3> cellsPositions) => 
            _spawnStrategy.GenerateBalls(cellsPositions);

        public BallColor GetBallColorForPosition(Vector3 cellWorldPosition) => 
            _spawnStrategy.GetBallColorForPosition(cellWorldPosition);
    }
}