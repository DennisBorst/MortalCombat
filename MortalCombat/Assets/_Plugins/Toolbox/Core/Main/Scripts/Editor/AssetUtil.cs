#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace ToolBox.Editor
{
    public static class AssetUtil
    {
        private const string EXTENSION = ".asset";

        public static bool Remove<T>(T asset) where T : ScriptableObject
        {
            return AssetDatabase.DeleteAsset(GetAssetPath(asset));
        }

        public static T Create<T>() where T : ScriptableObject
        {
            return (T)Create(typeof(T));
        }

        public static ScriptableObject Create(Type type)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets/";
            }
            else path += '/';
            return CreateAt(path, type);
        }

        public static T CreateAt<T>(string path, string assetName) where T : ScriptableObject
        {
            return (T)CreateAt(path, typeof(T), assetName);
        }

        public static T CreateAt<T>(string path) where T : ScriptableObject
        {
            return (T)CreateAt(path, typeof(T));
        }

        public static ScriptableObject CreateAt(string path, Type type)
        {
            return CreateAt(path, type, type.Name);
        }

        public static string GetPath(ScriptableObject path)
        {
            return AssetDatabase.GetAssetPath(path);
        }

        private static ScriptableObject CreateAt(string path, Type type, string assetName)
        {
            CreateDirectoryPathIfapplicable(path);
            var asset = ScriptableObject.CreateInstance(type);

            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(path + assetName + EXTENSION);
            AssetDatabase.CreateAsset(asset, uniquePath);
            AssetDatabase.Refresh();

            return asset;
        }

        private static void CreateDirectoryPathIfapplicable(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static bool Exists(string path, string assetName)
        {
            return File.Exists(path + assetName + EXTENSION);
        }

        public static string[] GetAssetGuids<T>() where T : ScriptableObject
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name.ToLower()}");
        }

        public static string[] GetAssetPaths<T>() where T : ScriptableObject
        {
            return GUIDSToAssetPaths(GetAssetGuids<T>());
        }

        public static string[] GUIDSToAssetPaths(string[] guids)
        {
            string[] paths = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                paths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
            }
            return paths;
        }

        public static string[] PathsToLabels(string[] paths)
        {
            string[] labels = new string[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                labels[i] = Path.GetFileNameWithoutExtension(paths[i]);
            }

            return labels;
        }

        public static T LoadAs<T>(UnityEngine.Object objectToLoad) where T : UnityEngine.Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(GetAssetPath(objectToLoad));
        }

        public static string GetAssetPath(UnityEngine.Object obj)
        {
            if (obj == null)
            {
                UnityEngine.Debug.LogError($"Couldn't get asset path of null.");
                return null;
            }

            if (!TryGetGuid(obj, out string guid))
                return null;

            return AssetDatabase.GUIDToAssetPath(guid);
        }

        public static bool TryGetGuid(UnityEngine.Object obj, out string guid)
        {
            if (obj == null)
            {
                UnityEngine.Debug.LogError($"Unable to fetch the guid of null");
                guid = string.Empty;
                return false;
            }

            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out long _))
            {
                return true;
            }

            UnityEngine.Debug.LogError($"Couldn't fetch GUID of {obj.name}. Does it exist as an asset?");
            return false;
        }
    }
}
#endif
