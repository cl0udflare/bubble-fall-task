using System.Collections.Generic;
using Gameplay.Core.Ball.Data;
using Gameplay.Core.Board.Data;
using Gameplay.Core.Board.StaticData;
using UnityEngine;

namespace Gameplay.Core.Board
{
    public interface IBoardSystem
    {
        void Initialize(BoardConfig config);
        BoardConfig GetBoardConfig();
        bool TryAddBall(Vector2Int gridPosition, BallColor color);
        bool RemoveBall(Vector2Int gridPosition);
        BoardBallData GetCell(Vector2Int gridPosition);
        IEnumerable<Vector2Int> GetAllCells();
        Vector3 GridToWorld(Vector2Int gridPosition);
        Vector3[] GetCellCorners(Vector2Int gridPosition);
    }
}