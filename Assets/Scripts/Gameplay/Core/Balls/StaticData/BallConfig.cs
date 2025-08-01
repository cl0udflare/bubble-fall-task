using UnityEngine;

namespace Gameplay.Core.Balls.StaticData
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Bubble Fall/Ball")]
    public class BallConfig : ScriptableObject
    {
        [SerializeField] private BallView _ballPrefab;
        public BallView BallPrefab => _ballPrefab;
    }
}