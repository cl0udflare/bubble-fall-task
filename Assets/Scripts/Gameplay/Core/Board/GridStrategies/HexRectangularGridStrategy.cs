using System.Collections.Generic;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Board.Data;
using Gameplay.Core.Board.Utils;
using UnityEngine;

namespace Gameplay.Core.Board.GridStrategies
{
    public class HexRectangularGridStrategy : IGridStrategy
    {
        private readonly Dictionary<Vector2Int, BoardBallData> _grid = new();
        private readonly int _rows, _cols;

        public HexRectangularGridStrategy(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
        }

        public bool TryAddCell(Vector2Int gridPos, BallColor color)
        {
            if (_grid.ContainsKey(gridPos)) return false;
            _grid[gridPos] = new BoardBallData { GridPosition = gridPos, Color = color };
            return true;
        }

        public bool RemoveCell(Vector2Int gridPos) => _grid.Remove(gridPos);

        public BoardBallData GetCell(Vector2Int gridPos)
            => _grid.GetValueOrDefault(gridPos);

        public IEnumerable<Vector2Int> AllCells()
        {
            for (int row = 0; row < _rows; row++)
            for (int col = 0; col < _cols; col++)
                yield return HexGridMath.OffsetToAxial(col, row);
        }

        public Vector3 GridToWorld(Vector2Int gridPos, float cellSize)
            => HexGridMath.AxialToWorld(gridPos, cellSize);

        public Vector3[] GetCell(Vector2Int gridPos, float cellSize)
        {
            Vector3 center = GridToWorld(gridPos, cellSize);
            return HexGridMath.GetHexCorners(center, cellSize);
        }
    }
}