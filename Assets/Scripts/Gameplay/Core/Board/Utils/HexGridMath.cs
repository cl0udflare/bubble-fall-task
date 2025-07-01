using UnityEngine;

namespace Gameplay.Core.Board.Utils
{
    public static class HexGridMath
    {
        public static Vector3 AxialToWorld(Vector2Int axial, float cellSize)
        {
            int q = axial.x;
            int r = axial.y;
            float x = cellSize * (q + r / 2f);
            float z = cellSize * Mathf.Sqrt(3f) / 2f * r;
            return new Vector3(x, 0f, -z);
        }

        public static Vector3[] GetHexCorners(Vector3 center, float cellSize)
        {
            float radius = cellSize * 0.5f;
            Vector3[] corners = new Vector3[6];
            for (int i = 0; i < 6; i++)
            {
                float angleDeg = 60f * i - 30f;
                float angleRad = Mathf.Deg2Rad * angleDeg;
                corners[i] = new Vector3(
                    center.x + radius * Mathf.Cos(angleRad),
                    0f,
                    center.z + radius * Mathf.Sin(angleRad)
                );
            }
            return corners;
        }
        
        public static Vector2Int OffsetToAxial(int col, int row)
        {
            int q = col - (row - (row & 1)) / 2;
            int r = row;
            return new Vector2Int(q, r);
        }

    }
}