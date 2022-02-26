using System.Collections;
using System.Collections.Generic;
using ToolBox.Editor;
using UnityEditor;
using UnityEngine;

namespace MortalCombat.CameraSystem
{
    [CustomEditor(typeof(BoundsRestriction))]
    public class DrawBoundsRestriction : Editor
    {
        private void OnSceneGUI()
        {
            var bounds = serializedObject.FindProperty("_Bounds");

            if (bounds == null)
            {
                Debug.LogError("Cound't find serialized _Bounds property in BoundsRestriction.");
                return;
            }

            EditorGUI.BeginChangeCheck();

            Rect rect = HandleUtil.DrawEditableRect2D(bounds.rectValue);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change bounds restriction");
                bounds.rectValue = rect;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}
