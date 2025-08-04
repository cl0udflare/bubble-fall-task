using System;
using Gameplay.Core.Levels.Data;
using UnityEngine;

namespace Gameplay.Core.Levels.StaticData
{
    [Serializable]
    public class BallSpawnSetup
    {
        [SerializeField] private BallSpawnType _spawnType;

        public BallSpawnType SpawnType => _spawnType;
    }
}