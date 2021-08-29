using UnityEngine;
using ToolBox;
using System;

namespace MortalCombat
{
    public class PlayerDeathMessage : Message
    {
        public readonly int playerId;

        public PlayerDeathMessage(int playerId)
        {
            this.playerId = playerId;
        }

        public override string ToString()
        {
            return $"Playerid {playerId} has died.";
        }
    }
}
