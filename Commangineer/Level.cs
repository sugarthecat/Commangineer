using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Transactions;

namespace Commangineer
{
    public class Level
    {
        private Tile[,] tiles;
        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        public Level(int level) {
            tiles = new Tile[100,100];
            Random generationRandom = new Random();
            for(int i = 0; i < 100; i++)
            {
                for(int j = 0; j < 100; j++)
                {
                    Point point = new Point(i, j);
                    int genNumber = generationRandom.Next(4);
                    if (genNumber == 0)
                    {
                        tiles[i, j] = new Tile(Assets.GetTexture("dirtystone"),point);
                    }
                    else if(genNumber == 1)
                    {
                        tiles[i, j] = new Tile(Assets.GetTexture("stone"), point);
                    }
                    else if (genNumber == 2)
                    {
                        tiles[i, j] = new Tile(Assets.GetTexture("oddstone"), point);
                    }
                    else if (genNumber == 3)
                    {
                        tiles[i, j] = new Tile(Assets.GetTexture("stoneTemp"), point);
                    }
                }
            }
        }
        /// <summary>
        /// Retreives how many tiles wide the level is
        /// </summary>
        /// <returns>How many tiles wide the level is</returns>
        public int GetTileWidth()
        {
            return tiles.GetLength(0);
        }
        /// <summary>
        /// Gets how many tiles tall the level is
        /// </summary>
        /// <returns>How many tiles tall the level is</returns>
        public int GetTileHeight()
        {
            return tiles.GetLength(1);
        }
        /// <summary>
        /// Render the level
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to render with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.GetImage("background"), new Rectangle(0, 0, Commangineer.GetScreenWidth(), Commangineer.GetScreenHeight()), Color.White);
            for(int i = 0; i<tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    Camera.Draw(spriteBatch, tiles[i, j]);
                    if (tiles[i, j].HasTileOccupier)
                    {
                        Camera.Draw(spriteBatch, tiles[i, j].GetTileOccupier());
                    }
                }
            }
            //TODO add draw function
        }
        /// <summary>
        /// Updates the level
        /// </summary>
        /// <param name="ms">Miliseconds since last updated</param>
        /// <param name="keyboardState">The current keyboard state</param>
        public void Update(int ms, KeyboardState keyboardState, KeyboardState previousKeyboardState,MouseState mouseState, MouseState previousMouseState)
        {
            if(mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue) { 
                Camera.UpdateScale(mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue);
            }
            Camera.UpdateMovement(keyboardState, ms);
        }
    }
}
