using Siren;
using UnityEngine;
using UnityEngine.Serialization;

namespace MortalCombat
{
    public class AudioOnAwake : MonoBehaviour
    {
        [SerializeField] private string AudioId;
        [SerializeField] private bool PlayOnce;

        static bool AlreadyPlayed = false;

        void Awake()
        {
            // TODO: Support one time play
            // in a more sober fashion. plz :D
            if (PlayOnce)
            {
                if (AlreadyPlayed)
                    return;
                AlreadyPlayed = true;
            }

            Audio.Play(AudioId);
        }
    }
}
