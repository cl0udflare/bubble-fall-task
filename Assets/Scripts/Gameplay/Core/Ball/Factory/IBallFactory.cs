using Gameplay.Core.Ball.Data;
using Gameplay.Core.Ball.StaticData;
using UnityEngine;

namespace Gameplay.Core.Ball.Factory
{
    public interface IBallFactory
    {
        BallView CreateBall(BallConfig config, Vector3 position, BallColor color);
    }
}