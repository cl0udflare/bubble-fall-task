using System.Collections.Generic;
using Gameplay.Core.Ball.BallPhysics.Utils;
using Gameplay.Core.Board;
using UnityEngine;

namespace Gameplay.Core.Ball.BallPhysics
{
    public class BallPhysicsSystem
    {
        private readonly IBoardSystem _boardSystem;

        public BallPhysicsSystem(IBoardSystem boardSystem) => 
            _boardSystem = boardSystem;

        public List<Vector2Int> GetDisconnectedBalls()
            => BallPhysicsUtils.FindDisconnectedBalls(_boardSystem);
    }
}
