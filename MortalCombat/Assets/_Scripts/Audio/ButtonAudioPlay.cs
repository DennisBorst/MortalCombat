using Siren;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MortalCombat
{
    public class ButtonAudioPlay : MonoBehaviour , ISelectHandler, ISubmitHandler, ICancelHandler
    {
        [SerializeField] private string AudioIdOnHover = "Menu/Hover";
        [SerializeField] private string AudioIdOnSubmit = "Menu/Confirm";
        [SerializeField] private string AudioIdOnCancel = "Menu/Cancel";

        public void OnCancel(BaseEventData eventData)
        {
            Audio.Play(AudioIdOnCancel);
        }

        public void OnSelect(BaseEventData eventData)
        {
            Audio.Play(AudioIdOnHover);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Audio.Play(AudioIdOnSubmit);
        }
    }
}
