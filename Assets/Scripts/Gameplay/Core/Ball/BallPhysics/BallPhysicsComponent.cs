using UnityEngine;

namespace Gameplay.Core.Ball.BallPhysics
{
    public class BallPhysicsComponent : MonoBehaviour
    {
        [SerializeField] private float _fallSpeed = 6f;
        private bool _isFalling;

        public void StartFalling() => 
            _isFalling = true;

        private void Update()
        {
            if (!_isFalling)
                return;

            transform.position += Vector3.back * (_fallSpeed * Time.deltaTime);
        }
    }
}