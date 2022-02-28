using UnityEngine;

namespace Siren
{
    /// <summary>
    /// Basically processes audio events and sends necessary information all over the place.
    /// </summary>
    public class SFXManager
    {
        private int AudioChannelLimit => AudioSettings.GetConfiguration().numVirtualVoices;
        private AudioAssetLibrary[] _Libraries;
        private AudioChannelPool _AudioChannelPool;

        public SFXManager()
        {
            _AudioChannelPool = new AudioChannelPool(AudioChannelLimit);
			_Libraries = FindAudioLibraries();

            AudioLog.Assert(_Libraries.Length > 0, "No audiolibraries found in root resources folder");
        }

        private AudioAssetLibrary[] FindAudioLibraries()
        {
            // Magically fish an AudioLibrary out of the resources.
            // I wish here was more dynamic functionality for this but Unity3D.
            return Resources.LoadAll<AudioAssetLibrary>("");
        }

        public void ProcessEvent(AudioEvent audioParams)
        {
            switch (audioParams.AudioCommand)
            {
                case AudioCommands.PLAY:
                    HandlePlayEvent(audioParams);
                    break;
                case AudioCommands.STOP_CONTEXT:
                    HandleStopEvent(audioParams);
                    break;
                default:
                    return;
            }
        }

        private void HandlePlayEvent(AudioEvent audioEvent)
        {
            if (TryResolveAsset(audioEvent, out AudioAssetContext assetContext))
                _AudioChannelPool.Play(assetContext, audioEvent);
        }

        private bool TryResolveAsset(AudioEvent audioEvent, out AudioAssetContext assetContext)
        {
            if (string.IsNullOrEmpty(audioEvent.Identifier))
            {
                AudioLog.Warning("Tried to resolve an audio asset with no identifier.");
                assetContext = default(AudioAssetContext);
                return false;
            }

			for (int i = 0; i < _Libraries.Length; i++) 
                if (_Libraries[i].TryResolve(audioEvent.Identifier, out assetContext)) 
                    return true; 

			AudioLog.Warning($"{audioEvent.Identifier} could not be found in any audio library.");
            assetContext = default(AudioAssetContext);
            return false;
        }

        private void HandleStopEvent(AudioEvent audioEvent)
        {
            _AudioChannelPool.StopContext(audioEvent.Context);
        }
    }
}
