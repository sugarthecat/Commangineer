using Commangineer.Floor_Auuki_types;
using Commangineer.Tile_Types;
using Commangineer.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;

namespace Commangineer
{
    public class Level
    {
        private Tile[,] tiles;
        Class1 test;
        Pathfinding pathing;

        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        public Level(int level)
        {
            JsonObject levelJSON = null;
            try
            {
                Random levelLoadRandom = new Random();
                string sources = Assembly.GetExecutingAssembly().Location + "/../Content";
                Debug.WriteLine(sources);
                string text = String.Join("", File.ReadAllLines(sources + "/levels/"  + "level1.json").Select(x => x.Trim()).ToArray());
                levelJSON = (JsonObject)JsonObject.Parse(text);
                int width = (int)levelJSON["width"];
                int height = (int)levelJSON["height"];
                string tileMapString = (string)levelJSON["tileMap"];
                string auukiMapString = (string)levelJSON["floorAuukiMap"];
                tiles = new Tile[width, height];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Point tilePoint = new Point(i, j);
                        switch (tileMapString[i * width + j])
                        {
                            case '0':
                                tiles[i, j] = new DirtTile(tilePoint);
                                break;
                            case '1':
                                tiles[i, j] = new StoneTile(tilePoint);
                                break;
                            case '2':
                                tiles[i, j] = new WaterTile(tilePoint);
                                break;
                            case '3':
                                tiles[i, j] = new DeepWaterTile(tilePoint);
                                break;
                            default:
                                tiles[i, j] = new DirtTile(tilePoint);
                                break;
                        }
                        switch (auukiMapString[i * width + j])
                        {
                            case '1':
                                tiles[i, j].InfectWithAuuki();
                                break;
                            case '2':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuuki().Age(1);
                                break;
                            case '3':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuuki().Age(5);
                                break;
                            case '4':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuuki().Age(10);
                                break;
                            case '5':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuuki().Age(30);
                                break;
                            default:
                                break;
                        }
                    }
                }
                
                pathing = new Pathfinding(tiles);
                test = new Class1(pathing);
            }
            catch (Exception ex)
            {
                //TODO log exceptions
                Commangineer.ExitGame();
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
            Camera.Draw(spriteBatch, test);
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
            test.update(deltaTime);
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