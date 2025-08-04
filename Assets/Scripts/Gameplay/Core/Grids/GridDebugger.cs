#if UNITY_EDITOR
using Gameplay.Core.Grids.StaticData;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Core.Grids
{
    [RequireComponent(typeof(GridSystem))]
    [ExecuteAlways]
    public class GridDebugger : MonoBehaviour
    {
        [SerializeField] private GridSystem _gridSystem;
        [SerializeField] private GridConfig _config;

        [Header("Settings")]
        [SerializeField] private bool _isDrawCellPosition;
        [SerializeField] private Color _cellColor = Color.cyan;

        private void OnValidate() => 
            _gridSystem = GetComponent<GridSystem>();

        private void OnEnable()
        {
            if (Application.isPlaying) return;
            if (_gridSystem != null)
                _gridSystem.Initialize(_config);
        }

        private void OnDrawGizmos()
        {
            if (!_gridSystem.IsInitialized()) return;
            if (_gridSystem == null || _config == null) return;

            Gizmos.color = _cellColor;

            foreach (Vector2Int gridPosition in _gridSystem.GetCells().Keys)
            {
                DrawCells(gridPosition);
                DrawPositions(gridPosition);
            }
        }

        private void DrawCells(Vector2Int gridPosition)
        {
            Vector3[] corners = _gridSystem.GetCellCorners(gridPosition);

            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 start = corners[i];
                Vector3 end = corners[(i + 1) % corners.Length];
                Gizmos.DrawLine(start, end);
            }
        }

        private void DrawPositions(Vector2Int gridPosition)
        {
            if (_isDrawCellPosition)
            {
                Vector3 center = _gridSystem.CellToWorld(gridPosition);
                Vector3 labelPos = center + Vector3.up * 0.05f;
                Handles.color = Color.white;
                Handles.Label(labelPos, $"{gridPosition.x},{gridPosition.y}");
            }
        }
    }
}
#endif