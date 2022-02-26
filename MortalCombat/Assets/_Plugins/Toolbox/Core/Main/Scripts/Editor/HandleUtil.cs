#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ToolBox.Editor
{
    public static class HandleUtil
    {
        public static Rect DrawEditableRect2D(Transform offsetTransform, Rect rect)
        {
            if (offsetTransform)
                return DrawEditableRect2D(offsetTransform.position, rect);
            return DrawEditableRect2D(Vector2.zero, rect);
        }

        public static Rect DrawEditableRect2D(Vector2 offset, Rect rect)
        {
            var drawRect = new Rect(rect.position + offset, rect.size);
            var resultRect = DrawEditableRect2D(drawRect);
            return new Rect(resultRect.position - offset, resultRect.size);
        }
        
        public static Rect DrawEditableRect2D(Rect rect)
        {
            RectCorners corners = new RectCorners(rect);
            Handles.DrawLine(corners.bottomLeft, corners.topLeft);
            Handles.DrawLine(corners.bottomRight, corners.topRight);
            Handles.DrawLine(corners.bottomLeft, corners.bottomRight);
            Handles.DrawLine(corners.topLeft, corners.topRight);

            return Rect.MinMaxRect(
                PositionHandle(corners.MiddleLeft).x,
                PositionHandle(corners.MiddleBottom).y,
                PositionHandle(corners.MiddleRight).x,
                PositionHandle(corners.MiddleTop).y);
        }

        private static Vector2 PositionHandle(Vector2 position)
        {
            return Handles.DoPositionHandle(position, Quaternion.identity);
        }
    }
}
#endif