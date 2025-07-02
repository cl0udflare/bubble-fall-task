using System;
using System.Collections.Generic;
using Gameplay.Core.Ball;
using Gameplay.Core.Board.Data;
using Gameplay.Core.Board.GridStrategies;
using Gameplay.Core.Board.StaticData;
using UnityEngine;

namespace Gameplay.Core.Board
{
    public class BoardSystem : IBoardSystem
    {
        private BoardConfig _config;
        private IGridStrategy _grid;
        
        public BoardConfig Config => _config;

        public void Initialize(BoardConfig config)
        {
            _config = config;
            CreateGridStrategy(config);
        }
        
        public bool TryAddCellData(Vector2Int cellPosition, out BoardData cellData)
            => _grid.TryAddCellData(cellPosition, out cellData);

        public bool TryRemoveCellData(Vector2Int gridPosition, out BallView ballView)
            => _grid.TryRemoveCellData(gridPosition, out ballView);

        public BoardData GetCell(Vector2Int gridPosition)
            => _grid.Cells.GetValueOrDefault(gridPosition);
        
        public Dictionary<Vector2Int, BoardData> GetCells()
            => _grid.Cells;
        
        public Dictionary<Vector2Int, BoardData> GetOccupiedCells()
            => _grid.GetOccupiedCells();

        public Vector3 CellToWorld(Vector2Int gridPosition)
            => _grid.CellToWorld(gridPosition);

        public Vector3[] GetCellCorners(Vector2Int gridPosition)
            => _grid.GetCellCorners(gridPosition);

        public IEnumerable<Vector2Int> GetNeighbors(Vector2Int position) => 
            _grid.GetNeighbors(position);

        public void Dispose()
        {
            _grid.Dispose();
            _config = null;
        }

        private void CreateGridStrategy(BoardConfig config)
        {
            _grid = config.GridType switch
            {
                GridType.Hex => new HexRectangularGridStrategy(config.Rows, config.Columns, config.CellSize),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _grid.GenerateCells();
        }
    }
}