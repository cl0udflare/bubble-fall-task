using System;
using Gameplay.Core.Grids.StaticData;
using Gameplay.Services.Time;
using UnityEngine;
using Zenject;

namespace Gameplay.Core.Grids
{
    public class GridMover : MonoBehaviour
    {
        private ITimeService _timeService;
        private GridMovementSetup _setup;
        private Vector3 _moveDirection;
        private float _cellSize;
        private float _timeElapsed;
        private float _distanceAccumulator;

        public event Action<int> OnCellShifted;
        
        [Inject]
        private void Construct(ITimeService timeService) => 
            _timeService = timeService;

        private void OnDestroy() => 
            _setup = null;

        public void Initialize(GridMovementSetup setup, float cellSize)
        {
            _setup = setup;
            _cellSize = cellSize;
            _timeElapsed = 0f;

            _moveDirection = setup.Direction switch
            {
                MoveDirection.Up => Vector3.forward,
                MoveDirection.Down => Vector3.back,
                MoveDirection.Left => Vector3.left,
                MoveDirection.Right => Vector3.right,
                MoveDirection.Unknown => Vector3.zero,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void Update()
        {
            if (_setup == null) return;

            float delta = ApplyMovement();
            CheckCellShift(delta);
        }

        private float ApplyMovement()
        {
            _timeElapsed += _timeService.DeltaTime;

            float normalizedTime = Mathf.Clamp01(_timeElapsed / _setup.TimeToMaxSpeedInSec);
            float curveMultiplier = _setup.SpeedCurve.Evaluate(normalizedTime);

            float currentSpeed = _setup.Speed * curveMultiplier;
            float deltaDistance = currentSpeed * _timeService.DeltaTime;

            transform.position += _moveDirection * deltaDistance;

            return deltaDistance;
        }
        
        private void CheckCellShift(float deltaDistance)
        {
            float rowStep = _cellSize * Mathf.Sqrt(3f) / 2f;
            _distanceAccumulator += deltaDistance;

            if (_distanceAccumulator >= rowStep)
            {
                int rowsPassed = Mathf.FloorToInt(_distanceAccumulator / rowStep);
                OnCellShifted?.Invoke(rowsPassed);
                _distanceAccumulator -= rowsPassed * rowStep;
            }
        }
    }
}