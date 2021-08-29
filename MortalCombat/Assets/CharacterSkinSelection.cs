using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class CharacterSkinSelection : MonoBehaviour
    {
        //[SerializeField] private GameObject currentSkin = null;
        [SerializeField] private GameObject character = null;
        [SerializeField] private CharacterID characterId = null;

        void Awake()
        {
            if (PlayerConfiguration.Instance == null)
                return;
            
            if (characterId)
            {
                GameObject skin = PlayerConfiguration.Instance.GetPlayerSkin(characterId.m_PlayerID);
                var obj = Instantiate(skin, character.transform, false);
                obj.name = "CharacterObject";

                character.SetActive(true);
            }
        }
    }
}
