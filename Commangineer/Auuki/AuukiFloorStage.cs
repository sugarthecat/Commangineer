using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.Auuki
{
    /// <summary>
    /// A stage of a floor Auuki
    /// </summary>
    public class FloorAuukiStage
    {
        private int maxHealth;
        private Texture2D texture;
        private float stageLength;

        // Creates a new stage of a floor Auuki
        public FloorAuukiStage(int maxHealth, Texture2D texture, float stageLength)
        {
            this.maxHealth = maxHealth;
            this.texture = texture;
            this.stageLength = stageLength;
        }
        // Creates a new stage of a floor auuki without a stage length
        public FloorAuukiStage(int maxHealth, Texture2D texture) : this(maxHealth, texture, 0)
        {
        }
        // Getters for the properties
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