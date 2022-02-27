#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ToolBox.Editor
{
    public static class SerializedPropertyExtensions 
    { 
        public static T[] ToArray<T>(this SerializedProperty property) where T : UnityEngine.Object
        {
            if (!property.isArray)
            {
                UnityEngine.Debug.LogError($"Unable to convert property/field '{property.name}' to an array: type '{property.type}' is not a collection.");
                return null;
            }

            T[] elements = new T[property.arraySize];
            for (int i = 0; i < property.arraySize; i++)
                elements[i] = (T)property.GetArrayElementAtIndex(i).objectReferenceValue;
            return elements;
        }

        public static T LoadAs<T>(this SerializedProperty property) where T : ScriptableObject
        {
            if (property.objectReferenceValue == null)
            {
                UnityEngine.Debug.LogError($"Cannot load property path {property.propertyPath} as {typeof(T).Name}: objectReverence is null");
            }

            return AssetUtil.LoadAs<T>(property.objectReferenceValue);
        }
    }
}
#endif