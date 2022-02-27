#if UNITY_EDITOR
using ToolBox;
using ToolBox.Editor;
using UnityEditor;

namespace Siren.Editor
{
    public static class AudioFileSystem
    {
        public static AudioAssetLibrary LoadLibrary(string path)
        {
            return AssetDatabase.LoadAssetAtPath<AudioAssetLibrary>(path);
        }

        public static AudioAssetLibrary[] LoadAllLibraries()
        {
            var paths = AssetUtil.GetAssetPaths<AudioAssetLibrary>();
            return paths.Select(LoadLibrary);
        }
    }
}
#endif