using System.Collections.Generic;
using Gameplay.Core.Board;
using Gameplay.Core.Board.Data;
using UnityEngine;

namespace Gameplay.Core.Ball.BallPhysics.Utils
{
    public static class BallPhysicsUtils
    {
        public static List<Vector2Int> FindDisconnectedBalls(IBoardSystem board)
        {
            HashSet<Vector2Int> connected = FindConnectedBalls(board);
            HashSet<Vector2Int> all = GetOccupiedPositions(board);
            all.ExceptWith(connected);

            return new List<Vector2Int>(all);
        }

        private static HashSet<Vector2Int> FindConnectedBalls(IBoardSystem board)
        {
            int columns = board.Config.Columns;
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            HashSet<Vector2Int> connected = new HashSet<Vector2Int>();
            Stack<Vector2Int> stack = new Stack<Vector2Int>();

            foreach (Vector2Int pos in GetTopRowPositions(columns))
            {
                BoardData cell = board.GetCell(pos);
                if (cell is { IsOccupied: true })
                {
                    stack.Push(pos);
                    connected.Add(pos);
                }
            }

            while (stack.Count > 0)
            {
                Vector2Int current = stack.Pop();
                if (!visited.Add(current)) continue;

                foreach (Vector2Int neighbor in board.GetNeighbors(current))
                {
                    BoardData neighborCell = board.GetCell(neighbor);
                    if (neighborCell is { IsOccupied: true } && connected.Add(neighbor))
                        stack.Push(neighbor);
                }
            }

            return connected;
        }

        private static IEnumerable<Vector2Int> GetTopRowPositions(int columns)
        {
            for (int col = 0; col < columns; col++)
                yield return new Vector2Int(col, 0);
        }

        private static HashSet<Vector2Int> GetOccupiedPositions(IBoardSystem board)
            => new(board.GetOccupiedCells().Keys);
    }
}