using Commangineer.AuukiStructures;
using Commangineer.AuukiStructures.Spawners;
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
        private GameAction[] gameActions;
        private AuukiStructure[] auukiStructures;
        private List<AuukiCreature> auukiCreatures;
        private MovingSquare debugSquare;
        private float gameTime = 0f;
        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        public Level(int level)
        {
            debugSquare = new MovingSquare();
            auukiCreatures = new List<AuukiCreature>();
            JsonObject levelJSON = null;
            try
            {
                Random levelLoadRandom = new Random();
                string sources = Assembly.GetExecutingAssembly().Location + "/../Content";
                //Debug.WriteLine(sources);
                string text = String.Join("", File.ReadAllLines(sources + "/levels/level" + level.ToString() + ".json").Select(x => x.Trim()).ToArray());
                levelJSON = (JsonObject)JsonObject.Parse(text);
                int width = (int)levelJSON["width"];
                int height = (int)levelJSON["height"];
                string tileMapString = (string)levelJSON["tileMap"];
                string auukiMapString = (string)levelJSON["floorAuukiMap"];
                auukiStructures = new AuukiStructure[0];
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

                            case '4':
                                tiles[i, j] = new MountainTile(tilePoint);
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
                                tiles[i, j].Auuki.Age(1);
                                break;

                            case '3':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].Auuki.Age(5);
                                break;

                            case '4':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].Auuki.Age(10);
                                break;

                            case '5':
                                tiles[i, j].InfectWithAuuki();
                                tiles[i, j].Auuki.Age(30);
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
                    switch (type) {
                        case 1:
                        outputStructure = new Bush(position);
                            break;
                        case 2:
                        outputStructure = new Tree(position);
                            break;
                        case 3:
                            outputStructure = new BigTree(position);
                            break;
                        case 5:
                            outputStructure = new RatSpawner(position);
                            break;
                        case 6:
                            outputStructure = new DeerSpawner(position);
                            break;
                        case 7:
                            outputStructure = new FoxSpawner(position);
                            break;
                        case 8:
                            outputStructure = new BearSpawner(position);
                            break;
                        default:
                            Log.LogText("Undefined Type at index" + i);
                            outputStructure = new Bush(position);
                            //invalid structure. mark as invalid and continue, logging innacuracy.
                            break;
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
                //Load actions
                JsonArray actions = levelJSON["actions"].AsArray();
                for (int i = 0; i < actions.Count; i++)
                {
                    switch (gameActions[i].GameValue)
                    {
                        case GameValue.GameTime:
                            gameActions[i].Update((int)gameTime);
                            break;
                        case GameValue.PlayerUnitCount:
                            gameActions[i].Update(0);
                            break;
                        case GameValue.AuukiUnitCount:
                            gameActions[i].Update(auukiCreatures.Count);
                            break;
                    }
                    if (gameActions[i].Active)
                    {
                        gameActions[i].Deactivate();
                    }
                }
                Log.LogText("Loaded level " + level + " succesfully.");
            }
            catch (Exception ex)
            {
                Log.LogText("Error Loading Level: " + ex.Message);
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
            DrawCreatures(spriteBatch);
            DrawStructures(spriteBatch);
            Camera.Draw(spriteBatch,debugSquare);
        }
        /// <summary>
        /// Draws the tiles within the level
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        private void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Camera.Draw(spriteBatch, tiles[i, j]);
                    if (tiles[i, j].HasAuukiTile)
                    {
                        Camera.Draw(spriteBatch, tiles[i, j].Auuki);
                    }
                }
            }
        }
        private void DrawStructures(SpriteBatch spriteBatch)
        {
            for(int i = 0; i<auukiStructures.Length; i++)
            {
                if (auukiStructures[i].Alive)
                {
                    Camera.Draw(spriteBatch, auukiStructures[i]);
                }
            }
        }
        private void DrawCreatures(SpriteBatch spriteBatch)
        {
            for(int i = 0; i<auukiCreatures.Count; i++)
            {
                Camera.Draw(spriteBatch, auukiCreatures[i]);
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
            gameTime += deltaTime;
            if (mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue)
            {
                Camera.UpdateScale(mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue);
            }
            Camera.UpdateMovement(keyboardState, ms); 
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                debugSquare.moveX(-deltaTime);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                debugSquare.moveX(deltaTime);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                debugSquare.moveY(-deltaTime);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                debugSquare.moveY(deltaTime);
            }
            GrowFloorAuuki(deltaTime);
            UpdateTiles(deltaTime);
            UpdateAuukiCreatures(deltaTime);
            UpdateAuukiStructures(deltaTime);
        }
        private void UpdateActions()
        {
            for(int i = 0; i < gameActions.Length; i++)
            {
                
            }
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
        /// Updates the auuki structures in the world
        /// </summary>
        /// <param name="deltaTime">The time since the last frame</param>
        private void UpdateAuukiStructures(float deltaTime)
        {
            for (int i = 0; i < auukiStructures.Length; i++)
            {
                auukiStructures[i].Update(deltaTime);
                if (auukiStructures[i] is Spawner && auukiStructures[i].Alive && ((Spawner)auukiStructures[i]).CanSpawnAnimal)
                {
                    auukiCreatures.Add(((Spawner)auukiStructures[i]).Animal);
                }
            }
        }
        /// <summary>
        /// Updates the auuki creatures in the world
        /// </summary>
        /// <param name="deltaTime">The time since the last frame</param>
        private void UpdateAuukiCreatures(float deltaTime)
        {
            for (int i = 0; i < auukiCreatures.Count; i++)
            {
                auukiCreatures[i].Update(deltaTime);
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