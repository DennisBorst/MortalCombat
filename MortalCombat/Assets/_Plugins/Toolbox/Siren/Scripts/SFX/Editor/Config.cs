#if UNITY_EDITOR

namespace Siren.Editor
{
    internal static class Config
    {
        public const string PATH_AUDIO_ROOT = "Assets/_Audio/_Siren/";
        public const string PATH_AUDIO_RESOURCES = PATH_AUDIO_ROOT + "Resources/";
        public const string PATH_AUDIO_LIBRARIES = PATH_AUDIO_RESOURCES + "AudioLibraries/";
        public const string PATH_AUDIO_ASSETS = PATH_AUDIO_RESOURCES + "AudioAssets/";
    }
}
#endif