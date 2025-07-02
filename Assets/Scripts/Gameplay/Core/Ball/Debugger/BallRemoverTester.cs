using System.Collections.Generic;
using Gameplay.Core.Ball.BallPhysics;
using UnityEngine;
using Gameplay.Core.Board;
using Gameplay.Core.Board.Data;

namespace Gameplay.Core.Ball.Debugger
{
    public class BallRemoverTester : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _ballLayer;
        [SerializeField] private float _gizmoLength = 100f;
        
        private BallPhysicsSystem _ballPhysicsSystem; 
        private IBoardSystem _boardSystem;

        private Vector3 _lastRayHit = Vector3.zero;
        private bool _hasHit = false;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryRemoveBallAtMouse();

            UpdateRaycastPreview();
        }

        public void CreatePhysicsSystem(IBoardSystem boardSystem)
        {
            _boardSystem = boardSystem;
            _ballPhysicsSystem = new BallPhysicsSystem(boardSystem);
        }

        private void TryRemoveBallAtMouse()
        {
            BallView ballView = RaycastBallUnderMouse();
            if (ballView == null) return;

            Vector2Int? gridPosition = FindGridPositionForBall(ballView);
            if (gridPosition.HasValue) RemoveBall(gridPosition.Value);
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
            print("BallView is null!");
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
            foreach (KeyValuePair<Vector2Int, BoardData> cells in _boardSystem.GetOccupiedCells())
            {
                BoardData cell = cells.Value;
                if (cell != null && cell.View == ballView)
                    return cells.Key;
            }
            return null;
        }

        private void RemoveBall(Vector2Int gridPosition)
        {
            if (_boardSystem.TryRemoveCellData(gridPosition, out BallView ballView))
            {
                print("Ball remove!");
                Destroy(ballView.gameObject);
            }

            DropDisconnectedBalls();
        }

        private void DropDisconnectedBalls()
        {
            foreach (Vector2Int pos in _ballPhysicsSystem.GetDisconnectedBalls())
            {
                BoardData cell = _boardSystem.GetCell(pos);
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
