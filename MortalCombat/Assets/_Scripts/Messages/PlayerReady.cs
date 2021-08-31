using ToolBox;

namespace MortalCombat
{
    public class PlayerReady : Message
    {
        public readonly int playerId;

        public PlayerReady(int playerId)
        {
            this.playerId = playerId;
        }
    }
}