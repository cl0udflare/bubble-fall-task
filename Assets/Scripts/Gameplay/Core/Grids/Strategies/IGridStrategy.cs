using System;
using System.Collections.Generic;
using Gameplay.Core.Balls;
using Gameplay.Core.Grids.Data;
using UnityEngine;

namespace Gameplay.Core.Grids.Strategies
{
    public interface IGridStrategy : IDisposable
    {
        Dictionary<Vector2Int, GridData> Cells { get; }
        int CurrentRowCount { get; }
        void GenerateCells();
        bool TryAddCellData(Vector2Int cellPosition, out GridData cellData);
        bool TryRemoveCellData(Vector2Int cellPosition, out BallView ballView);
        Dictionary<Vector2Int, GridData> GetOccupiedCells();
        Vector3[] GetCellCorners(Vector2Int cellPosition);
        Vector3 CellToWorld(Vector2Int cellPosition);
        IEnumerable<Vector2Int> GetNeighbors(Vector2Int position);
        void AddRowOnTop();
        void RemoveRowOnBottom();
    }
}