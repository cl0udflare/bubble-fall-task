using System.Collections.Generic;
using Gameplay.Core.Balls.BallPhysics.Utils;
using Gameplay.Core.Grids;
using UnityEngine;

namespace Gameplay.Core.Balls.BallPhysics
{
    public class BallPhysicsSystem
    {
        private readonly GridSystem _gridSystem;

        public BallPhysicsSystem(GridSystem gridSystem) => 
            _gridSystem = gridSystem;

        public List<Vector2Int> GetDisconnectedBalls()
            => BallPhysicsUtils.FindDisconnectedBalls(_gridSystem);
    }
}
