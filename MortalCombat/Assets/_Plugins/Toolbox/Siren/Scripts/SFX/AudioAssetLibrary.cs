using UnityEngine.Serialization;
using UnityEngine;
using ToolBox;

namespace Siren
{
    /// <summary>
    /// DataClass: Contains mappings from indentifier to audio asset
    /// </summary>
    public class AudioAssetLibrary : ScriptableObject
    {
        [FormerlySerializedAs("AudioIndentifier")]
        [SerializeField] private AudioIdentifierMapping[] _AudioAssetIdentifierMappings = new AudioIdentifierMapping[0];

        /// <summary>
        /// Resolves an indentifier to an audio asset.
        /// </summary>
        /// <Returns> Audioasset or null on fail</Returns>
        public AudioAsset Resolve(string identifier)
        {
            return _AudioAssetIdentifierMappings.ResolveOrDefault(identifier)?.AudioAsset;
        }
    }
}
