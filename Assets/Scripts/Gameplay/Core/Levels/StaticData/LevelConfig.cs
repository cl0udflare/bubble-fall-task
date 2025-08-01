using Gameplay.Core.Grids.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Core.Levels.StaticData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Bubble Fall/Level")]
    public class LevelConfig : ScriptableObject
    {
        [FormerlySerializedAs("_boardConfig")]
        [Header("Board Settings")]
        [SerializeField] private GridConfig gridConfig;
        [Space]
        [SerializeField] private int _minMatchGroupSize = 3; 
        
        public GridConfig GridConfig => gridConfig;
        public int MinMatchGroupSize => _minMatchGroupSize;
    }
}