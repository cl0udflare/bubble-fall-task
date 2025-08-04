using System.Collections.Generic;
using Gameplay.Core.Grids;
using Gameplay.Core.Grids.Data;
using UnityEngine;

namespace Gameplay.Core.Balls.BallPhysics.Utils
{
    public static class BallPhysicsUtils
    {
        public static List<Vector2Int> FindDisconnectedBalls(GridSystem grid)
        {
            HashSet<Vector2Int> connected = FindConnectedBalls(grid);
            HashSet<Vector2Int> all = GetOccupiedPositions(grid);
            all.ExceptWith(connected);

            return new List<Vector2Int>(all);
        }

        private static HashSet<Vector2Int> FindConnectedBalls(GridSystem grid)
        {
            int columns = grid.Config.GridSetup.Columns;
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            HashSet<Vector2Int> connected = new HashSet<Vector2Int>();
            Stack<Vector2Int> stack = new Stack<Vector2Int>();

            foreach (Vector2Int position in GetTopRowPositions(columns))
            {
                GridData cell = grid.GetCell(position);
                if (cell is { IsOccupied: true })
                {
                    stack.Push(position);
                    connected.Add(position);
                }
            }

            while (stack.Count > 0)
            {
                Vector2Int current = stack.Pop();
                if (!visited.Add(current)) continue;

                foreach (Vector2Int neighbor in grid.GetNeighbors(current))
                {
                    GridData neighborCell = grid.GetCell(neighbor);
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

        private static HashSet<Vector2Int> GetOccupiedPositions(GridSystem grid)
            => new(grid.GetOccupiedCells().Keys);
    }
}