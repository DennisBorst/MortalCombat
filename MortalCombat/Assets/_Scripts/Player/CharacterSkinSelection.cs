using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    [DefaultExecutionOrder(-1)]
    public class CharacterSkinSelection : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject currentSkin = null;
        [SerializeField] private GameObject character = null;
        [SerializeField] private CharacterID characterId = null;

        void Awake()
        {
            if (PlayerConfiguration.Instance == null)
                return;
            
            if (characterId)
            {
                if (currentSkin)
                    Object.Destroy(currentSkin);

                GameObject skin = PlayerConfiguration.Instance.GetPlayerSkin(characterId.m_PlayerID);
                var obj = Instantiate(skin, character.transform, false);
                obj.name = "CharacterObject";
                currentSkin = obj;

                if (animator)
                    StartCoroutine(RebindCoroutine());
            }
        }

        IEnumerator RebindCoroutine()
        {
            yield return null;
            if (animator)
                animator.Rebind();
        }
    }
}
