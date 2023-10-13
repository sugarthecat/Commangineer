using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    internal class FloorAuuki : TexturedObject
    {
        //default values
        private FloorAuukiStage[] stages;
        private int currentStageIndex;
        private float timeGrown = 0;
        private int health;
        private int maxHealth;
        private Texture2D texture;
        private Point position;
        private Point size;

        /// <summary>
        ///
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="maxHealth"></param>
        public FloorAuuki(FloorAuukiStage[] stages, Point position)
        {
            size = new Point(1, 1);
            currentStageIndex = 0;
            this.position = position;
            this.stages = stages;
            SetStage();
        }
        private void SetStage()
        {

            texture = stages[currentStageIndex].GetTexture();
            maxHealth = stages[currentStageIndex].GetHealth();
            health = maxHealth;
        }
        /// <summary>
        /// If the FloorAuuki is at full health
        /// </summary>
        public bool IsAtFullHealth
        {
            get { return health == maxHealth; }
        }

        /// <summary>
        /// If the FloorAuuki is dead
        /// </summary>
        public bool IsDead
        {
            get { return health <= 0; }
        }

        /// <summary>
        /// Takes a certain amount of damage
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.Draw(spriteBatch, this);
        }

        public Point GetSize()
        { return size; }

        public Point GetPosition()
        { return position; }

        public Texture2D GetTexture()
        { return texture; }

        /// <summary>
        /// Updates the tile
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            timeGrown += deltaTime;
            if(timeGrown > stages[currentStageIndex].GetStageLength() && currentStageIndex < stages.Length-1)
            {
                currentStageIndex++;
                SetStage();
                timeGrown = 0;
            }
        }
    }
}