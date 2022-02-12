using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class PlayerConfiguration : MonoBehaviour
    {
        public static PlayerConfiguration Instance;

        [SerializeField] private GameObject[] playerskins;
        [SerializeField] private Sprite[] playerSprites;
        private Dictionary<int, int> SelectedIndexPerPlayer = new Dictionary<int, int>(4);

        public void SetSelectedIndex(int playerId, int index)
        {
            SelectedIndexPerPlayer[playerId] = index;
        }

        public int GetSelectedIndex(int playerId)
        {
            if (SelectedIndexPerPlayer.TryGetValue(playerId, out int index))
                return index;

            return 0;
        }

        public GameObject GetPlayerSkin(int playerId)
        {
            return playerskins[SelectedIndexPerPlayer[playerId]];
        }

        public Sprite GetPlayerIcon(int playerId)
        {
            return playerSprites[SelectedIndexPerPlayer[playerId]];
        }

        private void Awake()
        {
            if (Instance != null) { 
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}