using System;
using Gameplay.Core.Balls;
using Gameplay.Core.Balls.Data;

namespace Gameplay.Core.Grids.Data
{
    public class GridData : IDisposable
    {
        public BallView View { get; set; }
        public BallColor Color { get; set; }

        public bool IsOccupied => View;

        public void Dispose()
        {
            View = null;
            Color = BallColor.Unknown;
        }
    }
}