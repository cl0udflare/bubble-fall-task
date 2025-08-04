using Gameplay.Core.Balls.Data;
using Gameplay.Core.Balls.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Core.Levels.SpawnStrategies
{
    public class ConcentricFlowStrategy : IBallSpawnStrategy
    {
        private readonly List<BallColor> _colors = BallColorUtils.Colors();
        private readonly float _cellSize;
        private readonly float _tileWidth;
        private readonly float _tileHeight;
        private readonly float _radius;

        private float _globalOffset;

        public ConcentricFlowStrategy(float cellSize, float tileWidth = 8f, float tileHeight = 8f, float radius = 3f)
        {
            _cellSize = cellSize;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _radius = radius;
        }

        public List<BallData> GenerateBalls(Dictionary<Vector2Int, Vector3> cellsPositions)
        {
            List<BallData> balls = new List<BallData>();
            foreach (KeyValuePair<Vector2Int, Vector3> positions in cellsPositions)
            {
                balls.Add(new BallData
                {
                    GridPosition = positions.Key,
                    Color = GetBallColorForPosition(positions.Value)
                });
            }

            return balls;
        }

        public BallColor GetBallColorForPosition(Vector3 cellWorldPosition)
        {
            float localX = cellWorldPosition.x % _tileWidth;
            float localZ = (cellWorldPosition.z + _globalOffset) % _tileHeight;
            if (localX < 0) localX += _tileWidth;
            if (localZ < 0) localZ += _tileHeight;

            float centerX = _tileWidth / 2f;
            float centerZ = _tileHeight / 2f;

            float dx = localX - centerX;
            float dz = localZ - centerZ;
            float distance = Mathf.Sqrt(dx * dx + dz * dz);

            return GetColorForDistance(distance, cellWorldPosition.x, cellWorldPosition.z);
        }

        public void ShiftPattern(int rowsAdded) =>
            _globalOffset += rowsAdded * _cellSize;

        private BallColor GetColorForDistance(float distance, float worldX, float worldZ)
        {
            float ringStep = _radius / 3f;
            int ringIndex = Mathf.FloorToInt(distance / ringStep);

            int patternOffset = Mathf.FloorToInt((worldZ + _globalOffset) / _tileHeight);

            float noiseScale = 0.28f;
            float noiseStrength = 2f;
            float noise = Mathf.PerlinNoise(worldX * noiseScale, worldZ * noiseScale) * noiseStrength;

            ringIndex += Mathf.FloorToInt(noise);

            int colorIndex = (ringIndex + patternOffset) % _colors.Count;
            if (colorIndex < 0)
                colorIndex += _colors.Count;

            return _colors[colorIndex];
        }
    }
}