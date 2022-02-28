using UnityEngine.Audio;

namespace Siren
{
    public struct AudioAssetContext
    {
		public AudioAssetLibrary sourceLibrary;
		public AudioAsset audioAsset;

        public AudioAssetContext(AudioAssetLibrary sourceLibrary, AudioAsset audioAsset)
        {
            this.sourceLibrary = sourceLibrary;
            this.audioAsset = audioAsset;
        }

        public AudioMixerGroup GetAudioMixerGroup()
        {
            if (audioAsset.AudioMixerGroup)
                return audioAsset.AudioMixerGroup;
            if (sourceLibrary.AudioMixerGroup)
                return sourceLibrary.AudioMixerGroup;
            return default;
        }
    }
}
