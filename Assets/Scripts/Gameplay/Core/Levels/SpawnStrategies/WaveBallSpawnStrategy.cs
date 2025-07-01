using System.Collections.Generic;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Ball.Utils;
using Gameplay.Services.Randoms;
using UnityEngine;

namespace Gameplay.Core.Levels.SpawnStrategies
{
    public class WaveBallSpawnStrategy : IBallSpawnStrategy
    {
        private readonly IRandomService _random;
        private readonly List<BallColor> _waveColors;
        private readonly float _dominanceProbability = 0.7f;

        public WaveBallSpawnStrategy(IRandomService random)
        {
            _random = random;
            _waveColors = BallColorUtils.Colors();
        }

        public List<BallData> GenerateBalls(IEnumerable<Vector2Int> gridLayout)
        {
            List<BallData> balls = new List<BallData>();
            int waveCount = _waveColors.Count;

            int cellIndex = 0;
            foreach (Vector2Int gridPosition in gridLayout)
            {
                int waveIndex = cellIndex % waveCount;
                BallColor dominantColor = _waveColors[waveIndex];

                BallColor color = _random.Value < _dominanceProbability
                    ? dominantColor
                    : GetRandomOtherColor(dominantColor);

                balls.Add(new BallData
                {
                    GridPosition = gridPosition,
                    Color = color
                });

                cellIndex++;
            }

            return balls;
        }

        private BallColor GetRandomOtherColor(BallColor exclude)
        {
            List<BallColor> colors = BallColorUtils.Colors();
            colors.Remove(exclude);
            return colors[_random.Range(0, colors.Count)];
        }
    }
}