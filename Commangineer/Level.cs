using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Commangineer
{
    public class Level
    {
        private Tile[,] tiles;

        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        public Level(int level)
        {
            tiles = new Tile[100, 100];
            Random generationRandom = new Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Point point = new Point(i, j);
                    int genNumber = generationRandom.Next(4);
                    if (genNumber == 0)
                    {
                        tiles[i, j] = new Tile(Assets.GetTexture("dirtystone"), point);
                    }
                    else if (genNumber == 1)
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
                    if (generationRandom.Next(1000) == 1)
                    {
                        tiles[i, j].InfectWithAuuki();
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
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Camera.Draw(spriteBatch, tiles[i, j]);
                    if (tiles[i, j].HasAuuki)
                    {
                        Camera.Draw(spriteBatch, tiles[i, j].GetAuuki());
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
        public void Update(int ms, KeyboardState keyboardState, KeyboardState previousKeyboardState, MouseState mouseState, MouseState previousMouseState)
        {
            float deltaTime = ms / 1000f;
            if (mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue)
            {
                Camera.UpdateScale(mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue);
            }
            Camera.UpdateMovement(keyboardState, ms);
            GrowFloorAuuki(deltaTime);
            UpdateTiles(deltaTime);
        }
        /// <summary>
        /// Updates the tiles in the world
        /// </summary>
        /// <param name="deltaTime">The time since the last frame</param>
        private void UpdateTiles(float deltaTime)
        {
            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].Update(deltaTime);
                }
            }
        }
        /// <summary>
        /// Grows the Auuki on the floor
        /// </summary>
        /// <param name="deltaTime">the seconds that have passed since the last update</param>
        private void GrowFloorAuuki(float deltaTime)
        {
            Random auukiGrowthRandom = new Random();
            bool[,] interactedWithTile = new bool[tiles.GetLength(0), tiles.GetLength(1)];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (!interactedWithTile[i, j] && tiles[i, j].HasAuuki)
                    {
                        for (int xOffset = -1; xOffset <= 1; xOffset++)
                        {
                            for (int yOffset = -1; yOffset <= 1; yOffset++)
                            {
                                if ((xOffset != 0 || yOffset != 0)
                                     && xOffset + i >= 0 && xOffset + i < tiles.GetLength(0)
                                     && yOffset + j >= 0 && yOffset + j < tiles.GetLength(1))
                                {
                                    if (!(tiles[i + xOffset, j + yOffset].HasAuuki || interactedWithTile[i + xOffset, j + yOffset]))
                                    {
                                      tiles[i + xOffset, j + yOffset].AttemptInfectWithAuuki(deltaTime);
                                     interactedWithTile[i + xOffset, j + yOffset] = true;
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}