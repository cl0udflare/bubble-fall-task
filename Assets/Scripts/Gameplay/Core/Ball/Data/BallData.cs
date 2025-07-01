using System;
using UnityEngine;

namespace Gameplay.Core.Ball.Data
{
    [Serializable]
    public class BallData
    {
        public Vector2Int GridPosition { get; set; }
        public BallColor Color { get; set; }
    }
}