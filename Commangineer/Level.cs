using Commangineer.AuukiStructures;
using Commangineer.Tile_Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        private AuukiStructure[] auukiStructures;
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
                string text = String.Join("", File.ReadAllLines(sources + "/levels/level" + level.ToString() + ".json").Select(x => x.Trim()).ToArray());
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
                                tiles[i, j].GetAuukiTile ().Age(1);
                                break;

                            case '3':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuukiTile().Age(5);
                                break;

                            case '4':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuukiTile().Age(10);
                                break;

                            case '5':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].GetAuukiTile().Age(30);
                                break;

                            default:
                                break;
                        }
                    }
                }
                JsonArray structures = levelJSON["structures"].AsArray();
                auukiStructures = new AuukiStructure[structures.Count];
                //Loop through and create an array for all structures
                for(int i = 0; i<structures.Count; i++)
                {
                    JsonNode structure = structures[i];
                    int xPos = (int)structure["x"];
                    int yPos = (int)structure["y"];
                    Point position = new Point(xPos, yPos);
                    int type = (int)structure["type"];
                    AuukiStructure outputStructure = null;
                    if(type == 1)
                    {
                        outputStructure = new Bush(position);
                    }
                    else if(type == 2)
                    {
                        outputStructure = new Tree(position);
                    }
                    else if(type == 3)
                    {
                        outputStructure = new BigTree(position);
                    }
                    else
                    {
                        //invalid structure. mark as invalid and continue, logging innacuracy.
                        continue;
                    }
                    auukiStructures[i] = outputStructure;
                    for(int x = 0; x<outputStructure.Size.X && x + xPos< tiles.GetLength(0); x++)
                    {
                        for (int y = 0; y < outputStructure.Size.Y && y + yPos < tiles.GetLength(1); y++)
                        {
                            outputStructure.AddTile(tiles[xPos + x, yPos + y]);
                        }
                    }
                }
            }
            catch (InvalidDataException ex)
            {
                Debug.WriteLine(ex.Message);
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
            DrawTiles(spriteBatch);
            DrawStructures(spriteBatch);
            //TODO add draw function
        }
        private void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Camera.Draw(spriteBatch, tiles[i, j]);
                    if (tiles[i, j].HasAuukiTile)
                    {
                        Camera.Draw(spriteBatch, tiles[i, j].GetAuukiTile());
                    }
                }
            }
        }
        private void DrawStructures(SpriteBatch spriteBatch)
        {
            for(int i = 0; i<auukiStructures.Length; i++)
            {
                Camera.Draw(spriteBatch,auukiStructures[i]);
            }
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
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
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
                    if (!interactedWithTile[i, j] && tiles[i, j].HasAuukiTile)
                    {
                        //for a square from (i-1,j-1) - (i+1,j+1), attempt to infect tiles with Auuki
                        for (int xOffset = -1; xOffset <= 1; xOffset++)
                        {
                            for (int yOffset = -1; yOffset <= 1; yOffset++)
                            {
                                if ((xOffset != 0 || yOffset != 0)
                                     && xOffset + i >= 0 && xOffset + i < tiles.GetLength(0)
                                     && yOffset + j >= 0 && yOffset + j < tiles.GetLength(1))
                                {
                                    if (!(tiles[i + xOffset, j + yOffset].HasAuukiTile || interactedWithTile[i + xOffset, j + yOffset]))
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