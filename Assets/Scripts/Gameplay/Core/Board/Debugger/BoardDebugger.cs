#if UNITY_EDITOR
using Gameplay.Core.Board.StaticData;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Core.Board.Debugger
{
    [ExecuteAlways]
    public class BoardDebugger : MonoBehaviour
    {
        [SerializeField] private BoardConfig _config;
        [SerializeField] private Color _cellColor = Color.cyan;
        [Header("Settings")]
        [SerializeField] private bool _isDrawCellPosition;

        private BoardSystem _boardSystem;

        private void OnEnable()
        {
            _boardSystem = new BoardSystem();
            _boardSystem.Initialize(_config);
        }

        private void OnDisable() => _boardSystem = null;

        private void OnDrawGizmos()
        {
            if (_boardSystem == null || _config == null) return;

            Gizmos.color = _cellColor;

            foreach (Vector2Int gridPosition in _boardSystem.GetAllCells())
            {
                Vector3[] corners = _boardSystem.GetCellCorners(gridPosition);

                for (int i = 0; i < corners.Length; i++)
                {
                    Vector3 start = transform.position + corners[i];
                    Vector3 end = transform.position + corners[(i + 1) % corners.Length];
                    Gizmos.DrawLine(start, end);
                }
                
                if (_isDrawCellPosition)
                {
                    Vector3 center = _boardSystem.GridToWorld(gridPosition);
                    Vector3 labelPos = transform.position + center + Vector3.up * 0.05f;
                    Handles.color = Color.white;
                    Handles.Label(labelPos, $"{gridPosition.x},{gridPosition.y}");
                }
            }
        }
    }
}
#endif