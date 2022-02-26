#if UNITY_EDITOR
using UnityEngine;

namespace ToolBox.Editor
{
    public struct RectCorners
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;

        public Vector2 MiddleLeft => (topLeft + bottomLeft) * 0.5f;
        public Vector2 MiddleRight => (topRight + bottomRight) * 0.5f;
        public Vector2 MiddleTop => (topLeft + topRight) * 0.5f;
        public Vector2 MiddleBottom => (bottomLeft + bottomRight) * 0.5f;

        public RectCorners(Rect rect)
        {
            topLeft = Rect.NormalizedToPoint(rect, new Vector2(0, 1));
            topRight = Rect.NormalizedToPoint(rect, new Vector2(1, 1));
            bottomLeft = Rect.NormalizedToPoint(rect, new Vector2(0, 0));
            bottomRight = Rect.NormalizedToPoint(rect, new Vector2(1, 0));
        }
    }
}
#endif