using ToolBox;

namespace MortalCombat
{
    public class PlayerDamagedMessage : Message
    {
        public readonly int playerId;
        public readonly float newHealth;

        public PlayerDamagedMessage(int playerId, float newHealth)
        {
            this.playerId = playerId;
            this.newHealth = newHealth;
        }
    }
}
