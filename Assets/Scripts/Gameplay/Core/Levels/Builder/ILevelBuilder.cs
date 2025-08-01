using Gameplay.Core.Balls.StaticData;
using Gameplay.Core.Grids;

namespace Gameplay.Core.Levels.Builder
{
    public interface ILevelBuilder
    {
        void Build();
        void Initialize(BallConfig ballConfig, GridSystem gridSystem);
    }
}