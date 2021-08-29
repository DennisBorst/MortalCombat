using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class PlayerConfiguration : MonoBehaviour
    {
        public static PlayerConfiguration Instance;

        [SerializeField] private GameObject[] playerskins;
        private Dictionary<int, int> SelectedIndexPerPlayer = new Dictionary<int, int>(4);

        public void SetSelectedIndex(int playerId, int index)
        {
            SelectedIndexPerPlayer[playerId] = index;
        }

        public GameObject GetPlayerSkin(int playerId)
        {
            return playerskins[SelectedIndexPerPlayer[playerId]];
        }

        private void Awake()
        {
            if (Instance != null) { 
                Destroy(this);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}