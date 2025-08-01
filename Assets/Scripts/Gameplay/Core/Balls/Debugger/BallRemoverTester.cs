using System.Collections.Generic;
using Gameplay.Core.Balls.BallPhysics;
using Gameplay.Core.Grids;
using Gameplay.Core.Grids.Data;
using UnityEngine;

namespace Gameplay.Core.Balls.Debugger
{
    public class BallRemoverTester : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _ballLayer;
        [SerializeField] private float _gizmoLength = 100f;

        private BallPhysicsSystem _ballPhysicsSystem;
        private GridSystem _gridSystem;

        private Vector3 _lastRayHit = Vector3.zero;
        private bool _hasHit = false;

        public void CreatePhysicsSystem(GridSystem gridSystem)
        {
            _gridSystem = gridSystem;
            _ballPhysicsSystem = new BallPhysicsSystem(gridSystem);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryRemoveBallGroupAtMouse();

            UpdateRaycastPreview();
        }

        private void TryRemoveBallGroupAtMouse()
        {
            BallView ballView = RaycastBallUnderMouse();
            if (ballView == null)
                return;

            Vector2Int? gridPosition = FindGridPositionForBall(ballView);
            if (gridPosition.HasValue)
                TryRemoveBallGroup(gridPosition.Value);
        }

        private BallView RaycastBallUnderMouse()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            _hasHit = false;

            if (Physics.Raycast(ray, out RaycastHit hit, _gizmoLength, _ballLayer))
            {
                _lastRayHit = hit.point;
                _hasHit = true;
                return hit.collider.GetComponent<BallView>();
            }

            _lastRayHit = ray.origin + ray.direction * _gizmoLength;
            return null;
        }

        private void UpdateRaycastPreview()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _gizmoLength, _ballLayer))
            {
                _lastRayHit = hit.point;
                _hasHit = true;
            }
            else
            {
                _lastRayHit = ray.origin + ray.direction * _gizmoLength;
                _hasHit = false;
            }
        }

        private Vector2Int? FindGridPositionForBall(BallView ballView)
        {
            foreach (KeyValuePair<Vector2Int, GridData> cellPair in _gridSystem.GetOccupiedCells())
            {
                GridData cell = cellPair.Value;
                if (cell != null && cell.View == ballView)
                    return cellPair.Key;
            }
            return null;
        }

        private void TryRemoveBallGroup(Vector2Int startPosition)
        {
            GridData startCell = _gridSystem.GetCell(startPosition);
            if (startCell == null || !startCell.IsOccupied)
                return;

            int minGroup = 3;
            List<Vector2Int> group = _gridSystem.FindMatchingGroup(startPosition, startCell.Color);

            if (group.Count < minGroup)
            {
                Debug.Log($"Not enough balls to remove! Group size: {group.Count}");
                return;
            }

            foreach (Vector2Int position in group)
            {
                if (_gridSystem.TryRemoveCellData(position, out BallView ballView) && ballView != null)
                    Destroy(ballView.gameObject);
            }

            DropDisconnectedBalls();
        }

        private void DropDisconnectedBalls()
        {
            foreach (Vector2Int position in _ballPhysicsSystem.GetDisconnectedBalls())
            {
                GridData cell = _gridSystem.GetCell(position);
                if (cell is { IsOccupied: true })
                    cell.View.BallPhysics.StartFalling();
            }
        }

        private void OnDrawGizmos()
        {
            if (_camera == null)
                return;

            Gizmos.color = _hasHit ? Color.green : Color.red;
            Gizmos.DrawLine(_camera.transform.position, _lastRayHit);

            if (_hasHit)
                Gizmos.DrawWireSphere(_lastRayHit, 0.25f);
        }
    }
}
