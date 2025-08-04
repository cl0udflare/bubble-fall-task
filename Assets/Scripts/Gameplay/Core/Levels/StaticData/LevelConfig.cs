using Gameplay.Core.Grids.StaticData;
using UnityEngine;

namespace Gameplay.Core.Levels.StaticData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Bubble Fall/Level")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Board Settings")]
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField, Space] private int _minMatchGroupSize = 3; 
        [SerializeField, Space] private BallSpawnSetup _spawnSetup; 
        
        public GridConfig GridConfig => _gridConfig;
        public int MinMatchGroupSize => _minMatchGroupSize;
        public BallSpawnSetup SpawnSetup => _spawnSetup;
    }
}