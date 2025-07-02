using System;
using Gameplay.Core.Ball.StaticData;
using Gameplay.Core.Board;

namespace Gameplay.Core.Levels.Builder
{
    public interface ILevelBuilder
    {
        void Build();
        void Initialize(BallConfig ballConfig, IBoardSystem boardSystem);
    }
}