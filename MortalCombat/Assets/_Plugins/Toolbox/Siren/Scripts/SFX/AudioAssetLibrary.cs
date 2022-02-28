using UnityEngine.Serialization;
using UnityEngine;
using ToolBox;
using UnityEngine.Audio;

namespace Siren
{
    /// <summary>
    /// DataClass: Contains mappings from indentifier to audio asset
    /// </summary>
    public class AudioAssetLibrary : ScriptableObject
    {
		[SerializeField] private AudioMixerGroup _AudioMixerGroup = null;
        [FormerlySerializedAs("AudioIndentifier")]
        [SerializeField] private AudioIdentifierMapping[] _AudioAssetIdentifierMappings = new AudioIdentifierMapping[0];

        public AudioMixerGroup AudioMixerGroup => _AudioMixerGroup;

        /// <summary>
        /// Attempts to find an audio asset using the provided ID
        /// </summary>
        public bool TryResolve(string identifier, out AudioAssetContext assetContext)
        {
            if (_AudioAssetIdentifierMappings.TryResolve(identifier, out AudioIdentifierMapping value))
            {
                assetContext = new AudioAssetContext(this, value.AudioAsset);
                return true;
            }
            assetContext = default(AudioAssetContext);
            return false;
        }

        /// <summary>
        /// Returns wether the audio asset is referenced in this library
        /// </summary>
        public bool ReferencesAudioAsset(AudioAsset asset)
        {
            return _AudioAssetIdentifierMappings.Any(x => x.AudioAsset == asset);
        }
    }
}
