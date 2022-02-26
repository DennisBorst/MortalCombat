using UnityEngine;

namespace ToolBox
{
    public static class Vector2Extensions
    {
        public static float Angle(this Vector2 src) => Mathf.Atan2(src.y, src.x);
        public static Vector2 MultipliyAxis(this Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
    }
}

