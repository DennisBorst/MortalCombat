using Siren;
using System.Collections.Generic;
using System.Linq;
using ToolBox.Services;
using UnityEngine;

namespace MortalCombat
{
    public class PlayerStatsService : MonoBehaviour, IBootstrapService
    {
        Dictionary<int, int> playerKills = new Dictionary<int, int>(4);

        public int GetKills(int playerId)
        {
            if (playerKills.TryGetValue(playerId, out int value))
                return value;
            return 0;
        }

        public void Addkills(int playerId, int amount)
        {
            if (playerKills.ContainsKey(playerId))
            {
                playerKills[playerId] += amount;
                return;
            }

            playerKills[playerId] = amount;
        }

        public void ResetKills()
        {
            playerKills.SetAll(0);
        }
    }
}