using ToolBox;

namespace MortalCombat
{
    public class PlayerSpawnMessage : Message
    {
        public readonly int playerId;
        public readonly float health;

        public PlayerSpawnMessage(int playerId, float health)
        {
            this.playerId = playerId;
            this.health = health;
        }
    }
}
