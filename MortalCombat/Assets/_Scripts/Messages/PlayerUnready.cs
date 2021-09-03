using ToolBox;

namespace MortalCombat
{
    public class PlayerUnready : Message
    {
        public readonly int playerId;

        public PlayerUnready(int playerId)
        {
            this.playerId = playerId;
        }
    }
}