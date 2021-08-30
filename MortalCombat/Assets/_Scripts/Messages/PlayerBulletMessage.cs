using ToolBox;


namespace MortalCombat
{
    public class PlayerBulletMessage : Message
    {
        public readonly int playerId;
        public readonly bool bulletReady;

        public PlayerBulletMessage(int playerId, bool bulletReady)
        {
            this.playerId = playerId;
            this.bulletReady = bulletReady;
        }
    }
}
