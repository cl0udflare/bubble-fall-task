using Gameplay.Core.Ball.BallPhysics;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Ball.Utils;
using UnityEngine;

namespace Gameplay.Core.Ball
{
    public class BallView : MonoBehaviour
    {
        private static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");
        
        [SerializeField] private BallPhysicsComponent _ballPhysics;
        [SerializeField] private MeshRenderer _renderer;

        private MaterialPropertyBlock _propertyBlock;

        public BallPhysicsComponent BallPhysics => _ballPhysics;

        private void Awake() => 
            _propertyBlock = new MaterialPropertyBlock();

        public void SetColor(BallColor color)
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(ColorProperty, BallColorUtils.ToColor(color));
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}