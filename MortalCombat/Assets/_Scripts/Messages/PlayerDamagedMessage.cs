using ToolBox;

namespace MortalCombat
{
    public class PlayerDamagedMessage : Message
    {
        public readonly int playerId;
        public readonly float damage;
        public readonly float oldHealth;
        public readonly float newHealth;
        public readonly float maxHealth;

        public PlayerDamagedMessage(int playerId, float damage, float oldHealth, float newHealth, float maxHealth)
        {
            this.playerId = playerId;
            this.damage = damage;
            this.oldHealth = oldHealth;
            this.newHealth = newHealth;
            this.maxHealth = maxHealth;
        }
    }
}
