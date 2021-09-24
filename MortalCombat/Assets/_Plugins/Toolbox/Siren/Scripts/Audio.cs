using System;
using System.Collections.Generic;
using UnityEngine;

namespace Siren
{
    /// <summary>
    /// Entrypoint for all audio related things, this should be the only thing you'd have to talk to.
    /// </summary>
    public static class Audio
    {
        private static readonly Lazy<SFXManager> s_SFXManager = new Lazy<SFXManager>(false);

        /// <summary>
        /// Used to send audio events inside of the audio 
        /// </summary>
        /// <param name="audioParams"></param>
        private static void SendSFXEvent(AudioEvent audioParams)
        {
            s_SFXManager.Value.ProcessEvent(audioParams);
        }

        public static void Play(string identifier)
        {
            SendSFXEvent(new AudioEvent(null, AudioCommands.PLAY, identifier));
        }

        public static void Play(string identifier, Transform location)
        {
            SendSFXEvent(new AudioEvent(null, AudioCommands.PLAY, identifier, followTransform: location));
        }

        public static void Play(string identifier, Vector3 worldPosition)
        {
            SendSFXEvent(new AudioEvent(null, AudioCommands.PLAY, identifier, worldPosition: worldPosition));
        }
    }
}

