using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer.Auuki
{
    /// <summary>
    /// A structure belonging to Auuki
    /// </summary>
    internal class AuukiStructure : TexturedObject, AuukiTarget
    {
        private Point position;
        private Point size;
        private Texture2D texture;
        private Tile[] tilesUnder;
        private int minFloorTier = 0;
        private bool alive = false;
        private int tileOn = 0;
        private int health = 0;
        // Creates a new Auuki Structure
        protected AuukiStructure(Point position, Point size, Texture2D texture, int minFloorTier, int health)
        {
            this.minFloorTier = minFloorTier;
            this.position = position;
            this.size = size;
            this.texture = texture;
            this.health = health;
            tilesUnder = new Tile[size.X * size.Y];
        }

        /// <summary>
        /// Updates the Auuki Structure
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public virtual void Update(float deltaTime)
        {
            if (!alive)
            {
                //if all supporting plants are alive, regrow.
                bool allTilesQualify = true;
                for (int i = 0; i < tilesUnder.Length; i++)
                {
                    if (!(tilesUnder[i].HasAuukiTile && tilesUnder[i].Auuki.Tier >= minFloorTier))
                    {
                        allTilesQualify = false;
                        break;
                    }
                }
                if (allTilesQualify)
                {
                    alive = true;
                }
            }
        }

        /// <summary>
        /// Damages the Auuki Structure
        /// </summary>
        /// <param name="damage">The amount of damage to deal to the Auuki Structure</param>
        public void Damage(int damage)
        {
            health -= damage;
            if(health <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Destroys the Auuki Structure
        /// </summary>
        private void Die()
        {
            alive = false;
            for (int i = 0; i < tilesUnder.Length; i++)
            {
                tilesUnder[i].RemoveAuuki();
            }
        }

        /// <summary>
        /// Gets if the structure is alive
        /// </summary>
        public bool Alive
        {
            get { return alive; }
        }

        /// <summary>
        /// Mark a certain tile as underneath the structure. Done on setup by the level.
        /// </summary>
        /// <param name="newTile"></param>
        public void AddTile(Tile newTile)
        {
            if (tileOn < tilesUnder.Length)
            {
                tilesUnder[tileOn] = newTile;
                newTile.AuukiStructure = this;
                tileOn++;
            }
        }

        /// <summary>
        /// Gets the structures size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(size.X, size.Y);
            }
        }

        /// <summary>
        /// Gets the structures position
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X, position.Y);
            }
        }

        /// <summary>
        /// Gets the structures center position
        /// </summary>
        public Vector2 CenterPosition
        {

            get
            {
                return Position + Size / 2;
            }
        }
        /// <summary>
        /// Gets the structures texture
        /// </summary>
        /// <returns>The structures texture</returns>
        public Texture2D GetTexture()
        {
            return texture;
        }
    }
}