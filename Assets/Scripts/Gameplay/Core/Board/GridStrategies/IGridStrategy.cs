using System;
using System.Collections.Generic;
using Gameplay.Core.Ball;
using Gameplay.Core.Board.Data;
using UnityEngine;

namespace Gameplay.Core.Board.GridStrategies
{
    public interface IGridStrategy : IDisposable
    {
        Dictionary<Vector2Int, BoardData> Cells { get; }
        void GenerateCells();
        bool TryAddCellData(Vector2Int cellPosition, out BoardData cellData);
        bool TryRemoveCellData(Vector2Int cellPosition, out BallView ballView);
        Dictionary<Vector2Int, BoardData> GetOccupiedCells();
        Vector3[] GetCellCorners(Vector2Int cellPosition);
        Vector3 CellToWorld(Vector2Int cellPosition);
        IEnumerable<Vector2Int> GetNeighbors(Vector2Int position);
    }
}