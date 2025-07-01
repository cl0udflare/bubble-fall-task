using System.Collections.Generic;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Board.Data;
using UnityEngine;

namespace Gameplay.Core.Board.GridStrategies
{
    public interface IGridStrategy
    {
        bool TryAddCell(Vector2Int gridPos, BallColor color);
        bool RemoveCell(Vector2Int gridPos);
        BoardBallData GetCell(Vector2Int gridPos);
        IEnumerable<Vector2Int> AllCells();
        Vector3 GridToWorld(Vector2Int gridPos, float cellSize);
        Vector3[] GetCell(Vector2Int gridPos, float cellSize);
    }
}