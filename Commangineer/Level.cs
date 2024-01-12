﻿using Commangineer.AuukiStructures;
using Commangineer.AuukiStructures.Spawners;
using Commangineer.GUI_Types;
using Commangineer.Tile_Types;
using Commangineer.Units;
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
        private PlayerBase playerBase;
        private GameAction[] gameActions;
        private List<DialogueGUI> dialogueGUIs;
        private AuukiStructure[] auukiStructures;
        private List<AuukiCreature> auukiCreatures;
        private List<Unit> playerUnits;
        private float gameTime = 0f;
        private float lastDialogueClick = 0f;
        private Pathfinding pathing;
        private LevelGUI currentGUI;
        private UnitEditorGUI unitEditor;

        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        /// /// <param name="levelGUI">The gui corresponding to the level</param>
        internal Level(int level, LevelGUI levelGUI)
        {
            currentGUI = levelGUI;
            unitEditor = new UnitEditorGUI();
            unitEditor.Enabled = false;
            dialogueGUIs = new List<DialogueGUI>();
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
                //
                JsonObject playerBaseJson = (JsonObject)levelJSON["playerBase"];
                playerBase = new PlayerBase(new Point((int)playerBaseJson["x"], (int)playerBaseJson["y"]));
                //load tiles
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
                                //creates spotty grass
                                if (levelLoadRandom.Next(0, 3) != 0)
                                {
                                    tiles[i, j].InfectWithAuuki();
                                    if (tiles[i, j].HasAuukiTile)
                                    {
                                        tiles[i, j].Auuki.Age((float)(levelLoadRandom.NextDouble() * 0.5f));
                                    }
                                }
                                break;

                            case '2':
                                tiles[i, j].InfectWithAuuki();
                                if (tiles[i, j].HasAuukiTile)
                                {
                                    tiles[i, j].Auuki.Age((float)(levelLoadRandom.NextDouble() * 1 + 0.5f));
                                }
                                break;

                            case '3':
                                tiles[i, j].InfectWithAuuki();
                                if (tiles[i, j].HasAuukiTile)
                                {
                                    tiles[i, j].Auuki.Age((float)(levelLoadRandom.NextDouble() * 2 + 3));
                                }
                                break;

                            case '4':
                                tiles[i, j].InfectWithAuuki();
                                if (tiles[i, j].HasAuukiTile)
                                {
                                    tiles[i, j].Auuki.Age((float)(levelLoadRandom.NextDouble() * 6 + 7));
                                }
                                break;

                            case '5':
                                tiles[i, j].InfectWithAuuki();
                                if (tiles[i, j].HasAuukiTile)
                                {
                                    tiles[i, j].Auuki.Age((float)(levelLoadRandom.NextDouble() * 10 + 20));
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
                //load structures
                auukiStructures = new AuukiStructure[0];
                JsonArray structures = levelJSON["structures"].AsArray();
                auukiStructures = new AuukiStructure[structures.Count];
                //Loop through and create an array for all structures
                for (int i = 0; i < structures.Count; i++)
                {
                    JsonNode structure = structures[i];
                    int xPos = (int)structure["x"];
                    int yPos = (int)structure["y"];
                    Point position = new Point(xPos, yPos);
                    int type = (int)structure["type"];
                    AuukiStructure outputStructure = null;
                    switch (type)
                    {
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
                    for (int x = 0; x < outputStructure.Size.X && x + xPos < tiles.GetLength(0); x++)
                    {
                        for (int y = 0; y < outputStructure.Size.Y && y + yPos < tiles.GetLength(1); y++)
                        {
                            outputStructure.AddTile(tiles[xPos + x, yPos + y]);
                        }
                    }
                }
                //Load actions
                JsonArray actions = levelJSON["actions"].AsArray();
                gameActions = new GameAction[actions.Count];
                for (int i = 0; i < actions.Count; i++)
                {
                    gameActions[i] = new GameAction((JsonObject)actions[i]);
                }
                Log.LogText("Loaded level " + level + " succesfully.");
                pathing = new Pathfinding(tiles);
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
            Camera.Draw(spriteBatch, playerBase);
            if (unitEditor.Enabled)
            {
                unitEditor.Draw(spriteBatch);
            }
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
            for (int i = 0; i < auukiStructures.Length; i++)
            {
                if (auukiStructures[i].Alive)
                {
                    Camera.Draw(spriteBatch, auukiStructures[i]);
                }
            }
        }

        private void DrawCreatures(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < auukiCreatures.Count; i++)
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
            if (unitEditor.Enabled)
            {
                unitEditor.Rescale();
            }
            else
            {
                if (dialogueGUIs.Count == 0)
                {
                    //only continue game if dialogue is over
                    gameTime += deltaTime;
                    UpdateActions();
                    GrowFloorAuuki(deltaTime);
                    UpdateTiles(deltaTime);
                    UpdateAuukiCreatures(deltaTime);
                    UpdateAuukiStructures(deltaTime);
                }
                if (mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue)
                {
                    Camera.UpdateScale(mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue);
                }
                Camera.UpdateMovement(keyboardState, ms);
            }
        }

        public void HandleClick(Point clickPosition)
        {
            if (unitEditor.Enabled)
            {

            }else if (dialogueGUIs.Count != 0)
            {
                lastDialogueClick = gameTime;
                DialogueGUI dialogue = dialogueGUIs[0];
                dialogue.Enabled = false;
                dialogue.RemoveAllGuiElements();
                dialogueGUIs.RemoveAt(0);
                if (dialogueGUIs.Count != 0)
                {
                    dialogueGUIs[0].Enabled = true;
                }
            }
        }

        public void HandleRightClick(Point clickPosition)
        {
            Vector2 adjustedClickPosition = Camera.DeprojectPoint(new Vector2(clickPosition.X, clickPosition.Y));
            if (playerBase.ContainsPoint(adjustedClickPosition) && unitEditor.Enabled == false)
            {
                unitEditor.Enabled = true;
            }
        }

        private void QueueDialogue(string text, string char1, string char2)
        {
            DialogueGUI dialogueGUI = new DialogueGUI();
            currentGUI.AddSubGUI(dialogueGUI);
            dialogueGUI.ChangeCharacter(1, char1 + "icon", true);
            if (char2 != null)
            {
                dialogueGUI.ChangeCharacter(2, char2 + "icon", false);
            }
            dialogueGUI.ChangeText(text);
            dialogueGUIs.Add(dialogueGUI);
            if (dialogueGUIs.Count == 1)
            {
                lastDialogueClick = gameTime;
                dialogueGUI.Enabled = true;
            }
        }

        private void UpdateActions()
        {
            for (int i = 0; i < gameActions.Length; i++)
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
                    foreach (Dictionary<string, string> gameEvent in gameActions[i].Events)
                    {
                        switch (gameEvent["eventType"])
                        {
                            case "dialogue":
                                string char2 = null;
                                if (gameEvent.ContainsKey("character2"))
                                {
                                    char2 = gameEvent["character2"];
                                }
                                QueueDialogue(gameEvent["speech"], gameEvent["character"], char2);
                                break;
                        }
                    }
                    //do stuff
                    gameActions[i].Deactivate();
                }
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
                auukiCreatures[i].Update(deltaTime, this);
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

        public bool Collides(AuukiCreature creature)
        {
            bool colliding = false;
            Point topLeft = new Point(
                 (int)Math.Floor(creature.Position.X),
                 (int)Math.Floor(creature.Position.Y)
                 );
            Point bottomRight = new Point(
             (int)Math.Ceiling(creature.Position.X + creature.Size.X),
             (int)Math.Ceiling(creature.Position.Y + creature.Size.Y)
                );
            //if any bit is out of bounds, it collides
            if (creature.Position.X < 0
                || creature.Position.Y < 0
                || creature.Position.X + creature.Size.X > tiles.GetLength(0) - 1
                || creature.Position.Y + creature.Size.Y > tiles.GetLength(1) - 1)
            {
                colliding = true;
                goto Collided;
            }

            //if any section is on a solid tile, it collides
            for (int i = topLeft.X; i < bottomRight.X; i++)
            {
                for (int j = topLeft.Y; j < bottomRight.Y; j++)
                {
                    if (tiles[i, j].IsSolid)
                    {
                        colliding = true;
                        goto Collided;
                        //goto is only valid way to exit nested loops, even though it's considered "weird"
                    }
                }
            }
        Collided:
            return colliding;
        }
    }
}