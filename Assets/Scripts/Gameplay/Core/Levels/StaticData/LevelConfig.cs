using Gameplay.Core.Board.StaticData;
using UnityEngine;

namespace Gameplay.Core.Levels.StaticData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Bubble Fall/Level")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Board Settings")]
        [SerializeField] private BoardConfig _boardConfig;
        
        public BoardConfig BoardConfig => _boardConfig;
    }
}