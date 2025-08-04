using Gameplay.Core.Balls.Data;
using Gameplay.Core.Balls.StaticData;
using UnityEngine;

namespace Gameplay.Core.Balls.Factory
{
    public class BallFactory : IBallFactory
    {
        private Transform _root;

        public BallView Create(BallConfig config, Vector3 position, BallColor color)
        {
            BallView ball = Object.Instantiate(config.BallPrefab, position, Quaternion.identity, _root);
            
            ball.SetColor(color);
            
            return ball;
        }

        public void SetRoot(Transform root) => 
            _root = root;
    }
}