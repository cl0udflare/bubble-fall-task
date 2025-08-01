using System;
using UnityEngine;

namespace Gameplay.Core.Grids.StaticData
{
    [Serializable]
    public class GridMovementSetup
    {
        [Header("Movement Settings")]
        [SerializeField] private MoveDirection _direction;
        [SerializeField] private float _speed = 1f;
        [Header("Acceleration Settings")]
        [SerializeField] private AnimationCurve _speedCurve = AnimationCurve.Linear(0, 1, 1, 1);
        [SerializeField] private float _timeToMaxSpeedInSec = 60f;
        
        public MoveDirection Direction => _direction;
        public float Speed => _speed;
        public AnimationCurve SpeedCurve => _speedCurve;
        public float TimeToMaxSpeedInSec => _timeToMaxSpeedInSec;
    }
    
    public enum MoveDirection
    {
        Unknown = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
}