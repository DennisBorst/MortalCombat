using System.Collections.Generic;
using ToolBox.Injection;
using UnityEngine;

namespace MortalCombat
{
    public class PlayerConfiguration : DependencyBehavior
    {
        public static PlayerConfiguration Instance;

        [Dependency] CharacterManagerService _CharacterManager;
        private CharacterConfiguration[] _Characters;
        private CharacterConfiguration[] Characters => _Characters ??= _CharacterManager.GetAllCharacters();

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
            return Characters[SelectedIndexPerPlayer[playerId]].Skin;
        }

        public Sprite GetPlayerIcon(int playerId)
        {
            return Characters[SelectedIndexPerPlayer[playerId]].CharacterSelectSprite;
        }

        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            base.Awake();

            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}