using UnityEngine;

namespace Gameplay.Services.Randoms
{
    public class RandomService : IRandomService
    {
        public float Value => Random.value;
        
        public float Range(float min, float max) => Random.Range(min, max);

        public int Range(int minInclusive, int maxExclusive) => Random.Range(minInclusive, maxExclusive);
    }
}