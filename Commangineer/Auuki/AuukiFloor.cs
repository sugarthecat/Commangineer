using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.Auuki
{
    /// <summary>
    /// A Auuki floor object
    /// </summary>
    internal class AuukiFloor : TexturedObject
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
        /// Creates a new Auuki Floor
        /// </summary>
        /// <param name="texture">The texture of the Auuki floor</param>
        /// <param name="position">The position of the Auuki floor</param>
        /// <param name="maxHealth">The maximum amount of health of the Auuki floor</param>
        public AuukiFloor(FloorAuukiStage[] stages, Point position)
        {
            size = new Point(1, 1);
            currentStageIndex = 0;
            this.position = position;
            this.stages = stages;
            SetStage();
        }

        /// <summary>
        /// Gets the current tier of the Auuki floor
        /// </summary>
        public int Tier
        {
            get
            {
                return currentStageIndex;
            }
        }

        /// <summary>
        /// Sets the stage of a Auuki Floor
        /// </summary>
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
        /// Draws a Auuki floor to the screen
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.Draw(spriteBatch, this);
        }

        /// <summary>
        /// Gets the Auuki floor's size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(size.X, size.Y);
            }
        }
        /// <summary>
        /// Gets the Auuki floor's position
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X, position.Y);
            }
        }
        /// <summary>
        /// Gets the Auuki floor's texture
        /// </summary>
        /// <returns>The Auuki floor's texture</returns>
        public Texture2D GetTexture()
        { return texture; }

        /// <summary>
        /// Updates the tile
        /// </summary>
        /// <param name="deltaTime">Time since the last update</param>
        public void Update(float deltaTime)
        {
            timeGrown += deltaTime;
            if (timeGrown > stages[currentStageIndex].GetStageLength() && currentStageIndex < stages.Length - 1)
            {
                currentStageIndex++;
                timeGrown -= stages[currentStageIndex].GetStageLength();
                SetStage();
            }
        }
        /// <summary>
        /// Age up the Floor Auuki
        /// </summary>
        /// <param name="minutes">The amount of minutes to age the auuki</param>
        public void Age(float minutes)
        {
            timeGrown = minutes * 60;
        }
    }
}