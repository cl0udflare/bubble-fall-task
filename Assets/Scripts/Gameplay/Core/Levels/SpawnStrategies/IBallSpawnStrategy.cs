using System.Collections.Generic;
using Gameplay.Core.Balls.Data;
using UnityEngine;

namespace Gameplay.Core.Levels.SpawnStrategies
{
    public interface IBallSpawnStrategy
    {
        List<BallData> GenerateBalls(IEnumerable<Vector2Int> gridLayout);
    }
}