using Gameplay.Core.Ball.Data;
using Gameplay.Core.Ball.StaticData;
using UnityEngine;

namespace Gameplay.Core.Ball.Factory
{
    public class BallFactory : IBallFactory
    {
        private readonly Transform _parent;

        public BallFactory()
        {
            _parent = new GameObject("[Balls]").transform;
        }

        public BallView CreateBall(BallConfig config, Vector3 position, BallColor color)
        {
            BallView ball = Object.Instantiate(config.BallPrefab, position, Quaternion.identity, _parent);
            ball.SetColor(color);
            return ball;
        }
    }
}