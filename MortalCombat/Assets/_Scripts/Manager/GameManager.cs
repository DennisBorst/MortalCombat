using System;
using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            GlobalEvents.AddListener<PlayerDeathMessage>(OnPlayerDeath);
        }

        private void OnDestroy()
        {
            GlobalEvents.RemoveListener<PlayerDeathMessage>(OnPlayerDeath);
        }

        private void OnPlayerDeath(PlayerDeathMessage obj)
        {
            // Announce to the rest of the game the opposite player has won
            switch (obj.playerId)
            {
                case 0: GlobalEvents.SendMessage(new PlayerWinMessage(1)); return;
                case 1: GlobalEvents.SendMessage(new PlayerWinMessage(0)); return;
            }
        }
    }
}