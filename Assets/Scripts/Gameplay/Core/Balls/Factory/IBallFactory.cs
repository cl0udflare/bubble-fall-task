using Gameplay.Core.Balls.Data;
using Gameplay.Core.Balls.StaticData;
using UnityEngine;

namespace Gameplay.Core.Balls.Factory
{
    public interface IBallFactory
    {
        BallView CreateBall(BallConfig config, Vector3 position, BallColor color);
    }
}