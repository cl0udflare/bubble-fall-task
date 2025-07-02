using System;
using Gameplay.Core.Ball;
using Gameplay.Core.Ball.Data;

namespace Gameplay.Core.Board.Data
{
    public class BoardData : IDisposable
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