using System;
using Gameplay.Core.Grids.Data;
using Gameplay.Core.Grids.StaticData;

namespace Gameplay.Core.Grids.Strategies
{
    public class GridStrategyCreator
    {
        private IGridStrategy _grid;

        public IGridStrategy CreateStrategy(GridSetup setup)
        {
            _grid = setup.GridType switch
            {
                GridType.Hex => new HexRectangularGridStrategy(setup.Rows, setup.Columns, setup.CellSize),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _grid.GenerateInitCells();
            
            return _grid;
        }
    }
}