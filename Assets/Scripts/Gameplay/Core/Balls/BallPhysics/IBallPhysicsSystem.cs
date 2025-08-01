using Gameplay.Core.Grids;

namespace Gameplay.Core.Balls.BallPhysics
{
    public interface IBallPhysicsSystem
    {
        void Initialize(GridSystem gridSystem);
        void DropDisconnectedBalls();
    }
}