using UnityEngine;

namespace MortalCombat.CameraSystem
{
    public static class CameraUtils
    {
        public static Rect Get2DCameraRect(this Camera camera)
        {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            Vector2 size = new Vector2(width, height);
            Vector2 position = ((Vector2)camera.transform.position) - (size * 0.5f);

            return new Rect(position, size);
        }
    }
}