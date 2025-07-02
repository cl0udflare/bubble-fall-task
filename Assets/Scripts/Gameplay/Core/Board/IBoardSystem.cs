using System;
using System.Collections.Generic;
using Gameplay.Core.Ball;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Board.Data;
using Gameplay.Core.Board.StaticData;
using UnityEngine;

namespace Gameplay.Core.Board
{
    public interface IBoardSystem : IDisposable
    {
        BoardConfig Config { get; }
        void Initialize(BoardConfig config);
        bool TryAddCellData(Vector2Int cellPosition, out BoardData cellData);
        bool TryRemoveCellData(Vector2Int gridPosition, out BallView ballView);
        BoardData GetCell(Vector2Int gridPosition);
        Dictionary<Vector2Int, BoardData> GetCells();
        Dictionary<Vector2Int, BoardData> GetOccupiedCells();
        Vector3 CellToWorld(Vector2Int gridPosition);
        Vector3[] GetCellCorners(Vector2Int gridPosition);
        IEnumerable<Vector2Int> GetNeighbors(Vector2Int position);
        List<Vector2Int> FindMatchingGroup(Vector2Int start, BallColor color);
    }
}