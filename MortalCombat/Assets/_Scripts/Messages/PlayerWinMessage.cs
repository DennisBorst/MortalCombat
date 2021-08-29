using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class PlayerWinMessage : Message
    {
        public readonly int playerId;

        public PlayerWinMessage(int playerId)
        {
            this.playerId = playerId;
        }

        public override string ToString()
        {
            return $"PlayerId {playerId} has won.";
        }
    }
}
