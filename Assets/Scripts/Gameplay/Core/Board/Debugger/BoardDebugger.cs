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

        public BoardSystem BoardSystem { get; private set; }

        private void OnEnable()
        {
            BoardSystem = new BoardSystem();
            BoardSystem.Initialize(_config);
        }

        private void OnDisable() => BoardSystem = null;

        private void OnDrawGizmos()
        {
            if (BoardSystem == null || _config == null) return;

            Gizmos.color = _cellColor;

            foreach (Vector2Int gridPosition in BoardSystem.GetCells().Keys)
            {
                Vector3[] corners = BoardSystem.GetCellCorners(gridPosition);

                for (int i = 0; i < corners.Length; i++)
                {
                    Vector3 start = transform.position + corners[i];
                    Vector3 end = transform.position + corners[(i + 1) % corners.Length];
                    Gizmos.DrawLine(start, end);
                }
                
                if (_isDrawCellPosition)
                {
                    Vector3 center = BoardSystem.CellToWorld(gridPosition);
                    Vector3 labelPos = transform.position + center + Vector3.up * 0.05f;
                    Handles.color = Color.white;
                    Handles.Label(labelPos, $"{gridPosition.x},{gridPosition.y}");
                }
            }
        }
    }
}
#endif