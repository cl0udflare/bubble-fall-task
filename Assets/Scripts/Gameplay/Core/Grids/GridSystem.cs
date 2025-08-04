using System;
using System.Collections.Generic;
using Gameplay.Core.Balls;
using Gameplay.Core.Balls.Data;
using Gameplay.Core.Grids.Data;
using Gameplay.Core.Grids.StaticData;
using Gameplay.Core.Grids.Strategies;
using Logging;
using UnityEngine;

namespace Gameplay.Core.Grids
{
    public class GridSystem : MonoBehaviour
    {
        private GridConfig _config;
        private IGridStrategy _grid;

        public GridConfig Config => _config;
        private bool _isInitialized => _grid != null;

        public event Action<List<Vector2Int>> OnCellAdded
        {
            add { if (IsInitialized()) _grid.OnCellAdded += value; }
            remove { if (IsInitialized()) _grid.OnCellAdded -= value; }
        }

        private void OnDestroy() =>
            _grid?.Dispose();

        public void Initialize(GridConfig config)
        {
            if (IsInitialized()) return;

            _config = config;

            GridStrategyCreator strategyCreator = new GridStrategyCreator();
            _grid = strategyCreator.CreateStrategy(_config.GridSetup);
        }

        public Dictionary<Vector2Int, GridData> GetCells() => _grid.Cells;

        public bool TryAddCellData(Vector2Int cellPosition, out GridData cellData) =>
            _grid.TryAddCellData(cellPosition, out cellData);

        public bool TryRemoveCellData(Vector2Int gridPosition, out BallView ballView) =>
            _grid.TryRemoveCellData(gridPosition, out ballView);

        public GridData GetCell(Vector2Int gridPosition) =>
            _grid.GetCell(gridPosition);

        public Dictionary<Vector2Int, GridData> GetOccupiedCells() =>
            _grid.GetOccupiedCells();

        public Vector3 CellToWorld(Vector2Int gridPosition) =>
            transform.position + _grid.CellToWorld(gridPosition);

        public Vector3[] GetCellCorners(Vector2Int gridPosition)
        {
            Vector3[] localCorners = _grid.GetCellCorners(gridPosition);
            for (int i = 0; i < localCorners.Length; i++)
                localCorners[i] += transform.position;

            return localCorners;
        }

        public IEnumerable<Vector2Int> GetNeighbors(Vector2Int position) =>
            _grid.GetNeighbors(position);

        public void AddNewRows(int count)
        {
            if (_grid == null) return;

            for (int i = 0; i < count; i++)
            {
                _grid.AddRowOnTop();
                _grid.RemoveRowOnBottom();
            }
        }

        public List<Vector2Int> FindMatchingGroup(Vector2Int start, BallColor color)
        {
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            List<Vector2Int> group = new List<Vector2Int>();
            Stack<Vector2Int> stack = new Stack<Vector2Int>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                Vector2Int position = stack.Pop();
                if (!visited.Add(position))
                    continue;

                GridData cell = _grid.GetCell(position);
                if (cell is not { IsOccupied: true } || cell.Color != color)
                    continue;

                group.Add(position);

                foreach (Vector2Int neighbor in _grid.GetNeighbors(position))
                {
                    if (!visited.Contains(neighbor))
                        stack.Push(neighbor);
                }
            }

            return group;
        }

        public bool IsInitialized()
        {
            if (_isInitialized) return true;
            
            DebugLogger.LogWarning($"{nameof(GridSystem)} isn't initialized", this);
            return false;
        }
    }
}