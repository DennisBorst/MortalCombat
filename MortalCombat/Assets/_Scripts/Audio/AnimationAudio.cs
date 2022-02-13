using Siren;
using UnityEngine;

namespace MortalCombat
{
    public class AnimationAudio : MonoBehaviour
    {
        public void Play(string audioId)
        {
            Audio.Play(audioId);
        }
    }
}
