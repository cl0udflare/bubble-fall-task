using System.Collections.Generic;
using Gameplay.Core.Ball;
using Gameplay.Core.Board.Data;
using Gameplay.Core.Board.Utils;
using Logging;
using UnityEngine;

namespace Gameplay.Core.Board.GridStrategies
{
    public class HexRectangularGridStrategy : IGridStrategy
    {
        private readonly Dictionary<Vector2Int, BoardData> _cells = new();
        private readonly int _rows, _cols;
        private readonly float _cellSize;

        public Dictionary<Vector2Int, BoardData> Cells => _cells;

        public HexRectangularGridStrategy(int rows, int cols, float cellSize)
        {
            _rows = rows;
            _cols = cols;
            _cellSize = cellSize;
        }

        public void GenerateCells()
        {
            foreach (Vector2Int gridPosition in AllCells())
            {
                BoardData boardData = new BoardData();
                _cells.Add(gridPosition, boardData);
            }
        }

        public bool TryAddCellData(Vector2Int cellPosition, out BoardData cellData)
        {
            if (_cells.TryGetValue(cellPosition, out cellData) && cellData != null)
                return true;

            DebugLogger.LogWarning(
                $"Cannot add cell data at {cellPosition}: cell is {(cellData == null ? "null" : "already occupied")}");
          
            cellData = null;
            return false;
        }

        public bool TryRemoveCellData(Vector2Int cellPosition, out BallView ballView)
        {
            if (!_cells.TryGetValue(cellPosition, out BoardData cellData))
            {
                DebugLogger.LogWarning($"Cannot remove cell data at {cellPosition}");
                ballView = null;
                return false;
            }

            ballView = cellData.View;
            cellData.Dispose();
            return true;
        }

        public Dictionary<Vector2Int, BoardData> GetOccupiedCells()
        {
            Dictionary<Vector2Int, BoardData> result = new Dictionary<Vector2Int, BoardData>();
            foreach (KeyValuePair<Vector2Int, BoardData> pair in _cells)
            {
                if (pair.Value is { IsOccupied: true })
                    result[pair.Key] = pair.Value;
            }
            
            return result;
        }
        
        public Vector3 CellToWorld(Vector2Int cellPosition)
            => HexGridMath.AxialToWorld(cellPosition, _cellSize);

        public Vector3[] GetCellCorners(Vector2Int cellPosition)
        {
            Vector3 center = CellToWorld(cellPosition);
            return HexGridMath.GetHexCorners(center, _cellSize);
        }

        public IEnumerable<Vector2Int> GetNeighbors(Vector2Int position)
        {
            yield return position + new Vector2Int(+1, 0);
            yield return position + new Vector2Int(-1, 0);
            yield return position + new Vector2Int(0, +1);
            yield return position + new Vector2Int(0, -1);
            yield return position + new Vector2Int(+1, -1);
            yield return position + new Vector2Int(-1, +1);
        }

        public void Dispose()
        {
            foreach (BoardData cellData in _cells.Values) 
                cellData.Dispose();
            
            _cells.Clear();
        }

        private IEnumerable<Vector2Int> AllCells()
        {
            for (int row = 0; row < _rows; row++)
            for (int col = 0; col < _cols; col++)
                yield return HexGridMath.OffsetToAxial(col, row);
        }
    }
}