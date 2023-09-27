using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer
{
    internal class Level
    {
        private Tile[,] tiles;
        int cameraX;
        int cameraY;
        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        public Level(int level) {
            tiles = new Tile[100,100];
            for(int i = 0; i < 100; i++)
            {
                for(int j = 0; j < 100; j++)
                {
                    tiles[i, j] = new Tile(Assets.GetTexture2D("smiley"));
                }
            }
        }
        /// <summary>
        /// Render the level
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to render with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.GetTexture2D("background"), new Rectangle(0, 0, Commangineer.instance.GetScreenWidth(), Commangineer.instance.GetScreenHeight()), Color.White);
            for(int i = 0; i<tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].Draw(spriteBatch, new Rectangle(i*10, j*10,10,10));
                }
            }
            //TODO add draw function
        }
        /// <summary>
        /// Updates the level
        /// </summary>
        /// <param name="ms">Miliseconds since last updated</param>
        public void Update(int ms)
        {

        }
    }
}
