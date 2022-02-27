#if UNITY_EDITOR
using UnityEditor;

namespace ToolBox
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
    }

}
#endif