using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Balls;
using Gameplay.Core.Grids.Data;
using Gameplay.Core.Grids.Utils;
using Logging;
using UnityEngine;

namespace Gameplay.Core.Grids.Strategies
{
    public class HexRectangularGridStrategy : IGridStrategy
    {
        private readonly Dictionary<Vector2Int, GridData> _cells = new();
        private readonly Stack<GridData> _poolGridData = new();
        private readonly int _rows, _cols;
        private readonly float _cellSize;
        
        private int _currentMaxRow; 
        private int _currentMinRow;
        
        public Dictionary<Vector2Int, GridData> Cells => _cells;
        public int CurrentRowCount => _currentMaxRow - _currentMinRow + 1;
        
        public event Action<List<Vector2Int>> OnCellAdded;

        public HexRectangularGridStrategy(int rows, int cols, float cellSize)
        {
            _rows = rows;
            _cols = cols;
            _cellSize = cellSize;
            
            _currentMaxRow = rows - 1;
            _currentMinRow = 0;
        }

        public void GenerateInitCells()
        {
            Dispose();
            
            foreach (Vector2Int gridPosition in AllCells())
            {
                GridData gridData = GetGridData();
                _cells.Add(gridPosition, gridData);
            }
            
            OnCellAdded?.Invoke(_cells.Keys.ToList());
        }

        public bool TryAddCellData(Vector2Int cellPosition, out GridData cellData)
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
            if (!_cells.TryGetValue(cellPosition, out GridData cellData))
            {
                DebugLogger.LogWarning($"Cannot remove cell data at {cellPosition}");
                ballView = null;
                return false;
            }

            ballView = cellData.View;
            cellData.Dispose();
            return true;
        }
        
        public GridData GetCell(Vector2Int gridPosition)
            => _cells.GetValueOrDefault(gridPosition);

        public Dictionary<Vector2Int, GridData> GetOccupiedCells()
        {
            Dictionary<Vector2Int, GridData> result = new Dictionary<Vector2Int, GridData>();
            foreach (KeyValuePair<Vector2Int, GridData> pair in _cells)
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
        
        public void AddRowOnTop()
        {
            List<Vector2Int> cells = new List<Vector2Int>();
            _currentMinRow--;

            for (int col = 0; col < _cols; col++)
            {
                Vector2Int gridPos = HexGridMath.OffsetToAxial(col, _currentMinRow);
                _cells[gridPos] = GetGridData();
                cells.Add(gridPos);
            }
            
            OnCellAdded?.Invoke(cells);
        }
        
        public void RemoveRowOnBottom()
        {
            for (int col = 0; col < _cols; col++)
            {
                Vector2Int gridPos = HexGridMath.OffsetToAxial(col, _currentMaxRow);
        
                if (_cells.TryGetValue(gridPos, out GridData data))
                {
                    ReleaseGridData(data);
                    _cells.Remove(gridPos);
                }
            }

            _currentMaxRow--;
        }

        public void Dispose()
        {
            foreach (GridData cellData in _cells.Values)
                ReleaseGridData(cellData);

            _cells.Clear();
        }

        private IEnumerable<Vector2Int> AllCells()
        {
            for (int row = 0; row < _rows; row++)
            for (int col = 0; col < _cols; col++)
                yield return HexGridMath.OffsetToAxial(col, row);
        }

        private GridData GetGridData() => 
            _poolGridData.Count > 0 ? _poolGridData.Pop() : new GridData();

        private void ReleaseGridData(GridData data)
        {
            data.Dispose();
            _poolGridData.Push(data);
        }
    }
}