using System.Collections.Generic;
using Gameplay.Core.Balls.Data;
using UnityEngine;

namespace Gameplay.Core.Levels.SpawnStrategies
{
    public interface IBallSpawnStrategy
    {
        List<BallData> GenerateBalls(Dictionary<Vector2Int, Vector3> cellsPositions);
        BallColor GetBallColorForPosition(Vector3 cellWorldPosition);
        void ShiftPattern(int rowsAdded);
    }
}