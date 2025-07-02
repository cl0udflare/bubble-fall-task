using Gameplay.Core.Board;

namespace Gameplay.Core.Ball.BallPhysics
{
    public interface IBallPhysicsSystem
    {
        void Initialize(IBoardSystem boardSystem);
        void DropDisconnectedBalls();
    }
}