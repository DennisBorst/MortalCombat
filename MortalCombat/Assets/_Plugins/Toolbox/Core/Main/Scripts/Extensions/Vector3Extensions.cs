using UnityEngine;

namespace ToolBox
{
    public static class Vector3Extensions
    {
        public static Vector2 XZ(this Vector3 src) => new Vector2(src.x, src.z);
        public static Vector2 XY(this Vector3 src) => new Vector2(src.x, src.y);

        public static Vector3 MultiplyAxis(this Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

        public static Vector3 GetRandom(this Vector3 src)
        {
            return new Vector3(
                Random.Range(-src.x, src.x),
                Random.Range(-src.y, src.y),
                Random.Range(-src.z, src.z));
        }
    }
}
