using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ToolBox
{
    public static class ObjectExtensions
    {
        public static T SpawnOn<T>(this T behaviour, Transform position) where T : Object
            => Object.Instantiate(behaviour, position.position, position.rotation);
        public static T SpawnOn<T>(this T behaviour, Vector3 position) where T : Object
            => Object.Instantiate(behaviour, position, Quaternion.identity);
        public static void NullCheck<T1, T2>(this T1 behavior, string name, T2 value) where T1 : Object where T2 : Component
        {
            if (value == null)
                UnityEngine.Debug.LogError($"Variable '{name}' of type '{typeof(T2).Name}' hasn't been assigned on object '{behavior.name}'!", behavior);
        }
    }
}