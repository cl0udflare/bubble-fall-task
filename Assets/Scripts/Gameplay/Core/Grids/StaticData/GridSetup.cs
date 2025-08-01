using System;
using Gameplay.Core.Grids.Data;
using UnityEngine;

namespace Gameplay.Core.Grids.StaticData
{
    [Serializable]
    public class GridSetup
    {
        [Header("Grid Settings")]
        [SerializeField] private GridType _gridType;
        [SerializeField] private int _columns = 10;
        [SerializeField] private int _rows = 10;
        [SerializeField] private float _cellSize = 1f;
        
        public GridType GridType => _gridType;
        public int Columns => _columns;
        public int Rows => _rows;
        public float CellSize => _cellSize;
    }
}