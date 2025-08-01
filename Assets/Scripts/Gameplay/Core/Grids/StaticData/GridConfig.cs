using UnityEngine;

namespace Gameplay.Core.Grids.StaticData
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Bubble Fall/Grid")]
    public class GridConfig : ScriptableObject
    {
        [SerializeField,      ] private GridSetup _gridSetup;
        [SerializeField, Space] private GridMovementSetup _movementSetup;

        public GridSetup GridSetup => _gridSetup;
        public GridMovementSetup MovementSetup => _movementSetup;
    }
}