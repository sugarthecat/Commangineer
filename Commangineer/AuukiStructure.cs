using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Commangineer
{
    internal class AuukiStructure : TexturedObject
    {
        private Point position;
        private Point size;
        private Texture2D texture;
        private Tile[] tilesUnder;
        private int minFloorTier = 0;
        private bool alive = false;
        private int tileOn = 0;
        protected AuukiStructure(Point position, Point size, Texture2D texture, int minFloorTier)
        {
            this.minFloorTier = minFloorTier;
            this.position = position;
            this.size = size;
            this.texture = texture;
            tilesUnder = new Tile[size.X * size.Y];
        }

        public virtual void Update(float deltaTime)
        {
            if (alive)
            {
            }
            else
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
                tileOn++;
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(size.X,size.Y);
            }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X, position.Y);
            }
        }

        public Texture2D GetTexture()
        {
            return texture;
        }
    }
}