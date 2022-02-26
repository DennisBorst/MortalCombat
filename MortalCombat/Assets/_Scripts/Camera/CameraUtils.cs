using UnityEngine;

namespace MortalCombat.CameraSystem
{
    public static class CameraUtils
    {
        public static Rect Get2DCameraRect(this Camera camera)
        {
            Vector2 size = GetOrtographicSizeVec2D(camera.orthographicSize, camera.aspect);
            Vector2 position = ((Vector2)camera.transform.position) - (size * 0.5f);

            return new Rect(position, size);
        }

        public static Vector2 GetOrtographicSizeVec2D(float ortographicSize, float aspect)
        {
            float height = 2f * ortographicSize;
            float width = height * aspect;
            return new Vector2(width, height);
        }
    }
}