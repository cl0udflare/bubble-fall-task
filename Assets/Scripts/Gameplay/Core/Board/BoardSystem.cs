using System;
using System.Collections.Generic;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Board.Data;
using Gameplay.Core.Board.GridStrategies;
using Gameplay.Core.Board.StaticData;
using UnityEngine;

namespace Gameplay.Core.Board
{
    public class BoardSystem : IBoardSystem
    {
        private IGridStrategy _grid;
        private float _cellSize;
        private BoardConfig _config;

        public void Initialize(BoardConfig config)
        {
            _config = config;
            _cellSize = config.CellSize;

            CreateGridStrategy(config);
        }

        public BoardConfig GetBoardConfig() => _config;

        public bool TryAddBall(Vector2Int gridPosition, BallColor color)
            => _grid.TryAddCell(gridPosition, color);

        public bool RemoveBall(Vector2Int gridPosition)
            => _grid.RemoveCell(gridPosition);

        public BoardBallData GetCell(Vector2Int gridPosition)
            => _grid.GetCell(gridPosition);

        public IEnumerable<Vector2Int> GetAllCells()
            => _grid.AllCells();

        public Vector3 GridToWorld(Vector2Int gridPosition)
            => _grid.GridToWorld(gridPosition, _cellSize);

        public Vector3[] GetCellCorners(Vector2Int gridPosition)
            => _grid.GetCell(gridPosition, _cellSize);

        private void CreateGridStrategy(BoardConfig config)
        {
            _grid = config.GridType switch
            {
                GridType.Hex => new HexRectangularGridStrategy(config.Rows, config.Columns),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}