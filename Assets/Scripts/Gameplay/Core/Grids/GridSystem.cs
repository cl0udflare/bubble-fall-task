using System.Collections.Generic;
using Gameplay.Core.Balls;
using Gameplay.Core.Balls.Data;
using Gameplay.Core.Grids.Data;
using Gameplay.Core.Grids.StaticData;
using Gameplay.Core.Grids.Strategies;
using UnityEngine;

namespace Gameplay.Core.Grids
{
    [RequireComponent(typeof(GridMover))]
    public class GridSystem : MonoBehaviour
    {
        [SerializeField] private GridConfig _config;
        [SerializeField] private GridMover _gridMover;

        private IGridStrategy _grid;

        public GridConfig Config => _config;
        public GridMover GridMover => _gridMover;
        public bool IsInitialized => _grid != null;

        public void OnDestroy()
        {
            _grid?.Dispose();
            _gridMover.OnCellShifted -= AddNewRows;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
            
            GridStrategyCreator strategyCreator = new GridStrategyCreator();
            _grid = strategyCreator.CreateStrategy(_config.GridSetup);

            _gridMover.OnCellShifted += AddNewRows;
            _gridMover.Initialize(_config.MovementSetup, _config.GridSetup.CellSize);
        }

        public bool TryAddCellData(Vector2Int cellPosition, out GridData cellData)
            => _grid.TryAddCellData(cellPosition, out cellData);

        public bool TryRemoveCellData(Vector2Int gridPosition, out BallView ballView)
            => _grid.TryRemoveCellData(gridPosition, out ballView);

        public GridData GetCell(Vector2Int gridPosition)
            => _grid.Cells.GetValueOrDefault(gridPosition);

        public Dictionary<Vector2Int, GridData> GetCells()
            => _grid.Cells;

        public Dictionary<Vector2Int, GridData> GetOccupiedCells()
            => _grid.GetOccupiedCells();

        public Vector3 CellToWorld(Vector2Int gridPosition)
            => _grid.CellToWorld(gridPosition);

        public Vector3[] GetCellCorners(Vector2Int gridPosition)
            => _grid.GetCellCorners(gridPosition);

        public IEnumerable<Vector2Int> GetNeighbors(Vector2Int position) =>
            _grid.GetNeighbors(position);

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

                GridData cell = GetCell(position);
                if (cell is not { IsOccupied: true } || cell.Color != color)
                    continue;

                group.Add(position);

                foreach (Vector2Int neighbor in GetNeighbors(position))
                {
                    if (!visited.Contains(neighbor))
                        stack.Push(neighbor);
                }
            }

            return group;
        }

        private void AddNewRows(int count)
        {
            if (_grid == null) return;
            
            for (int i = 0; i < count; i++)
            {
                _grid.AddRowOnTop();
                _grid.RemoveRowOnBottom();
            }
        }
    }
}

