using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.Auuki
{
    internal class FloorAuukiStage
    {
        private int maxHealth;
        private Texture2D texture;
        private float stageLength;

        public FloorAuukiStage(int maxHealth, Texture2D texture, float stageLength)
        {
            this.maxHealth = maxHealth;
            this.texture = texture;
            this.stageLength = stageLength;
        }
        public FloorAuukiStage(int maxHealth, Texture2D texture) : this(maxHealth, texture, 0) { }

        public int GetHealth()
        {
            return maxHealth;
        }
        public Texture2D GetTexture()
        {
            return texture;
        }

        public float GetStageLength()
        {
            return stageLength;
        }
    }
}