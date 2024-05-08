using Commangineer.Auuki;
using Commangineer.Auuki.AuukiStructures;
using Commangineer.Auuki.AuukiStructures.Spawners;
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
using RectangleF = System.Drawing.RectangleF;

namespace Commangineer
{
    /// <summary>
    /// A class which handles most interactions with a level
    /// </summary>
    public class Level
    {
        private Tile[,] tiles;
        private PlayerBase playerBase;
        private GameAction[] gameActions;
        private List<DialogueGUI> dialogueGUIs;
        private AuukiStructure[] auukiStructures;
        private List<AuukiCreature> auukiCreatures = new List<AuukiCreature>();
        private List<Unit> playerUnits = new List<Unit>();
        private Queue<Unit> playerUnitQueue = new Queue<Unit>();
        private List<Unit> selectedUnits = new List<Unit>();
        private MaterialBalance resources = new MaterialBalance();
        private AuukiAiMode SpawningAuukiAiMode = AuukiAiMode.Wander;
        private float gameTime = 0f;
        private LevelGUI currentGUI;
        private UnitEditorGUI unitEditor;
        private int levelNum;
        private bool winDialogueFinished;

        private MaterialBalance income = new MaterialBalance();
        private bool selectingZone = false;
        private Vector2 zoneSelectionStart;

        /// <summary>
        /// Initializes the level
        /// </summary>
        /// <param name="level">The specific level to initialize (Level 1, 2, etc)</param>
        /// /// <param name="levelGUI">The gui corresponding to the level</param>
        public Level(int level, LevelGUI levelGUI)
        {
            levelNum = level;
            winDialogueFinished = false;
            currentGUI = levelGUI;
            unitEditor = new UnitEditorGUI();
            unitEditor.Enabled = false;
            dialogueGUIs = new List<DialogueGUI>();
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

                //load player base
                JsonObject playerBaseJson = (JsonObject)levelJSON["playerBase"];
                playerBase = new PlayerBase(new Point((int)playerBaseJson["x"], (int)playerBaseJson["y"]));

                //load tiles
                tiles = new Tile[width, height];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Point tilePoint = new Point(i, j);
                        switch (tileMapString[i * height + j])
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
                        switch (auukiMapString[i * height + j])
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
                for (int i = 0; i < auukiStructures.Length; i++)
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
                //load income
                JsonNode resources = levelJSON["resources"];
                income = new MaterialBalance(resources);
                //Load actions=
                JsonArray actions = levelJSON["events"].AsArray();
                gameActions = new GameAction[actions.Count];
                for (int i = 0; i < actions.Count; i++)
                {
                    gameActions[i] = new GameAction((JsonObject)actions[i]);
                }
                Log.LogText("Loaded level " + level + " succesfully.");
            }
            catch (Exception ex)
            {
                Log.LogText("Error Loading Level: " + ex.Message + "," + ex.InnerException + "," + ex.StackTrace);
                //TODO log exceptions
                Commangineer.ExitGame();
            }
        }
        public int LevelID
        {
            get
            {
                return levelNum;
            }
        }
        /// <summary>
        /// Gets a target to a Unit at a position within a certain range
        /// </summary>
        /// <param name="position">The position to search for a Unit at</param>
        /// <param name="range">The range of how far away to search for a Unit</param>
        /// <returns>The first Unit found</returns>
        public PlayerTarget GetUnitTarget(Vector2 position, float range)
        {
            PlayerTarget closestTarget = playerBase;
            
            for (int i = 0; i < playerUnits.Count; i++)
            {
                if ((position - playerUnits[i].CenterPosition).Length() < (position - closestTarget.CenterPosition).Length())
                {
                    closestTarget = playerUnits[i];
                }
            }
            return closestTarget;
        }
        /// <summary>
        /// Gets a target to a Auuki at a position within a certain range
        /// </summary>
        /// <param name="position">The position to search for a Auuki at</param>
        /// <param name="range">The range of how far away to search for a Auuki</param>
        /// <returns>The first Auuki found</returns>
        public AuukiTarget GetAuukiTarget(Vector2 position, float range)
        {
            for (int i = 0; i < auukiCreatures.Count; i++)
            {
                if ((position - auukiCreatures[i].CenterPosition).Length() < range)
                {
                    return auukiCreatures[i];
                }
            }
            Point pointPos = position.ToPoint();
            Rectangle rectRange = new Rectangle(pointPos.X - (int)range, pointPos.Y - (int)range, (int)(range * 2), (int)(range * 2));
            for (int i = Math.Max(rectRange.X, 0); i < Math.Min(rectRange.Right, GetTileWidth()); i++)
            {
                for (int j = Math.Max(rectRange.Y, 0); j < Math.Min(rectRange.Bottom, GetTileHeight()); j++)
                {
                    if (tiles[i, j].HasAuukiStructure)
                    {
                        return tiles[i, j].AuukiStructure;
                    }
                }
            }
            for (int i = Math.Max(rectRange.X, 0); i < Math.Min(rectRange.Right, GetTileWidth()); i++)
            {
                for (int j = Math.Max(rectRange.Y, 0); j < Math.Min(rectRange.Bottom, GetTileHeight()); j++)
                {
                    if (tiles[i, j].HasAuukiTile && (position - tiles[i, j].Position).Length() < range)
                    {
                        return tiles[i, j].Auuki;
                    }
                }
            }
            return null;
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
        /// Gets the tile map for the level
        /// </summary>
        /// <returns>an array of the level's tiles</returns>
        public Tile[,] GetTileMap()
        {
            return tiles;
        }

        /// <summary>
        /// Selects units with a boundary
        /// </summary>
        /// <param name="unprojectedSelectionRange">The boundary to check for units</param>
        private void SelectNewUnits(RectangleF unprojectedSelectionRange)
        {
            RectangleF projectedSelection = Camera.Deproject(unprojectedSelectionRange);
            selectedUnits = new List<Unit>();
            foreach (Unit unit in playerUnits)
            {
                if (unit.Intersects(projectedSelection))
                {
                    selectedUnits.Add(unit);
                }
            }
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
            DrawUnits(spriteBatch);
            DrawStructures(spriteBatch);
            Camera.Draw(spriteBatch, playerBase);
            if (unitEditor.Enabled)
            {
                unitEditor.Draw(spriteBatch);
            }
            else if (selectingZone && dialogueGUIs.Count == 0)
            {
                MouseState mouseState = Mouse.GetState();
                spriteBatch.Draw(Assets.GetImage("selectZone"), SelectionRectangle, Color.White);
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

        /// <summary>
        /// Draws the structures within the level
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
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

        /// <summary>
        /// Draws the creatures within the level
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw with</param>
        private void DrawCreatures(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < auukiCreatures.Count; i++)
            {
                Camera.Draw(spriteBatch, auukiCreatures[i]);
            }
        }

        /// <summary>
        /// Draws all player units
        /// </summary>
        /// <param name="spriteBatch">the spriteBatch to draw them in</param>
        private void DrawUnits(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                playerUnits[i].Draw(spriteBatch);
            }
            for (int i = 0; i < playerUnits.Count; i++)
            {
                playerUnits[i].DrawBulletFrames(spriteBatch);
            }
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnits[i].Alive)
                {
                    selectedUnits[i].DrawSelection(spriteBatch);
                }
                else
                {
                    selectedUnits.RemoveAt(i);
                    i--;
                }
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
            currentGUI.Update(resources);
            if (unitEditor.Enabled)
            {
                unitEditor.Update();
            }
            else
            {
                //only continue game if dialogue is over
                if (dialogueGUIs.Count == 0)
                {
                    if (Math.Floor(gameTime + deltaTime) != Math.Floor(gameTime))
                    {
                        //if new second,give 10 scrap, 2 iron, 5 tungsten. Add to editor.
                        resources += income;
                    }
                    gameTime += deltaTime;
                    UpdateActions();
                    GrowFloorAuuki(deltaTime);
                    UpdateTiles(deltaTime);
                    UpdateAuukiCreatures(deltaTime);
                    UpdateAuukiStructures(deltaTime);
                    UpdatePlayerUnits(deltaTime);
                }
                else
                {
                    dialogueGUIs[0].Update();
                }
                if (mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue)
                {
                    Camera.UpdateScale(mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue);
                }
                if (selectingZone && (keyboardState.IsKeyUp(Keys.LeftShift) || mouseState.LeftButton == ButtonState.Released || dialogueGUIs.Count > 0))
                {
                    selectingZone = false;
                    SelectNewUnits(SelectionRectangleF);
                }
                Camera.UpdateMovement(keyboardState, ms);
            }
        }

        /// <summary>
        /// Updates the player's units
        /// </summary>
        /// <param name="deltaTime">The amount of time passed since last update</param>
        private void UpdatePlayerUnits(float deltaTime)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                if (playerUnits[i].Alive)
                {
                    playerUnits[i].Update(deltaTime, this);
                }
                else
                {
                    playerUnits.RemoveAt(i);
                    i--;
                }
                
            }
            if (playerUnitQueue.Count > 0 && !Collides(playerUnitQueue.Peek()))
            {
                playerUnits.Add(playerUnitQueue.Dequeue());
            }
        }

        /// <summary>
        /// Returns a rectangle used for unit selection boundaries
        /// </summary>
        private Rectangle SelectionRectangle
        {
            get
            {
                Point mousePosition = Mouse.GetState().Position;
                Point start = new Point(
                    Math.Min(mousePosition.X, (int)zoneSelectionStart.X),
                    Math.Min(mousePosition.Y, (int)zoneSelectionStart.Y)
                    );
                Point end = new Point(
                    Math.Max(mousePosition.X, (int)zoneSelectionStart.X),
                    Math.Max(mousePosition.Y, (int)zoneSelectionStart.Y)
                    );
                return new Rectangle(start, end - start);
            }
        }

        /// <summary>
        /// Returns a rectangleF used for unit selection boundaries
        /// </summary>
        private RectangleF SelectionRectangleF
        {
            get
            {
                Point mousePosition = Mouse.GetState().Position;
                Vector2 start = new Vector2(
                    Math.Min(mousePosition.X, (int)zoneSelectionStart.X),
                    Math.Min(mousePosition.Y, (int)zoneSelectionStart.Y)
                    );
                Vector2 end = new Vector2(
                    Math.Max(mousePosition.X, (int)zoneSelectionStart.X),
                    Math.Max(mousePosition.Y, (int)zoneSelectionStart.Y)
                    );
                Vector2 size = end - start;
                return new RectangleF(start.X, start.Y, size.X, size.Y);
            }
        }

        /// <summary>
        /// Handles left click interactions within the level
        /// </summary>
        /// <param name="clickPosition">The position the user is clicking</param>
        /// <param name="shiftEnabled">If the shift key is being held down</param>
        public void HandleClick(Point clickPosition, bool shiftEnabled)
        {
            if (unitEditor.Enabled)
            {
                unitEditor.HandleClick(clickPosition);
            }
            else if (dialogueGUIs.Count != 0)
            {
                DialogueGUI dialogue = dialogueGUIs[0];
                dialogue.Enabled = false;
                dialogue.RemoveAllGuiElements();
                dialogueGUIs.RemoveAt(0);
                if (dialogueGUIs.Count != 0)
                {
                    dialogueGUIs[0].Enabled = true;
                }
            }
            else if (shiftEnabled)
            {
                selectingZone = true;
                zoneSelectionStart = clickPosition.ToVector2();
            }
        }

        /// <summary>
        /// Handles all right click interactions with the level
        /// </summary>
        /// <param name="clickPosition">The position the player is clicking</param>
        public void HandleRightClick(Point clickPosition)
        {
            Vector2 adjustedClickPosition = Camera.Deproject(new Vector2(clickPosition.X, clickPosition.Y));
            if (playerBase.ContainsPoint(adjustedClickPosition) && unitEditor.Enabled == false)
            {
                unitEditor.Enabled = true;
            }
            else
            {
                //find average position
                Vector2 averagePosition = new Vector2(0, 0);
                
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    averagePosition += selectedUnits[i].Position;
                }
                averagePosition /= selectedUnits.Count;
                for(int i = 0; i<selectedUnits.Count; i++)
                {
                    selectedUnits[i].Goal = adjustedClickPosition + ((selectedUnits[i].CenterPosition - averagePosition) * new Vector2(0.7f)) - selectedUnits[i].Size/2;
                }
            }
        }

        /// <summary>
        /// Adds a dialogue popup to a queue of popups
        /// </summary>
        /// <param name="text">The message to display</param>
        /// <param name="char1">The character on the left</param>
        /// <param name="char2">The character on the right</param>
        private void QueueDialogue(string text, string char1, string char2)
        {
            DialogueGUI dialogueGUI = new DialogueGUI();
            currentGUI.AddSubGUI(dialogueGUI);
            // Note that the third parameter, is if the character is active (not grey'd out), here we assume char1 is always the first one active
            dialogueGUI.ChangeCharacter(1, char1 + "icon", true);
            if (char2 != null)
            {
                dialogueGUI.ChangeCharacter(2, char2 + "icon", false);
            }
            dialogueGUI.ChangeText(text);
            dialogueGUIs.Add(dialogueGUI);
            if (dialogueGUIs.Count == 1)
            {
                dialogueGUI.Enabled = true;
            }
        }

        /// <summary>
        /// Updates all game actions
        /// </summary>
        private void UpdateActions()
        {
            for (int i = 0; i < gameActions.Length; i++)
            {
                // Updates action requirements
                switch (gameActions[i].GameValue)
                {
                    case GameValue.GameTime:
                        gameActions[i].Update((int)gameTime);
                        break;

                    case GameValue.PlayerUnitCount:
                        gameActions[i].Update(playerUnits.Count);
                        break;

                    case GameValue.AuukiUnitCount:
                        gameActions[i].Update(auukiCreatures.Count);
                        break;
                    case GameValue.WonDialogue:
                        gameActions[i].Update(Convert.ToInt32(this.Won));
                        break;
                    case GameValue.Won:
                        gameActions[i].Update(Convert.ToInt32(this.Won && dialogueGUIs.Count == 0 && winDialogueFinished));
                        break;
                }
                // If a action is activated, call it's respected event
                if (gameActions[i].Active)
                {
                    // Loads in events like dialogue popups
                    foreach (Dictionary<string, string> gameEvent in gameActions[i].Events)
                    {
                        switch (gameEvent["actionType"])
                        {
                            case "dialogue":
                                string char2 = null;
                                if (gameEvent.ContainsKey("character2"))
                                {
                                    char2 = gameEvent["character2"];
                                }
                                QueueDialogue(gameEvent["speech"], gameEvent["character"], char2);
                                break;
                            case "ai":
                                switch ((string)gameEvent["behavior"])
                                {
                                    case "wander":
                                        SpawningAuukiAiMode = AuukiAiMode.Wander;
                                        break;
                                    case "attack":
                                        SpawningAuukiAiMode = AuukiAiMode.Attack;
                                        break;
                                }
                                break;
                            case "win":
                                Commangineer.WinLevel(levelNum);
                                break;
                            case "finishWinDialogue":
                                winDialogueFinished = true;
                                break;
                        }
                    }
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
                    AuukiCreature newAnimal = ((Spawner)auukiStructures[i]).Animal;
                    newAnimal.Behavior = SpawningAuukiAiMode;
                    auukiCreatures.Add(newAnimal);
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
                AuukiCreature creature = auukiCreatures[i];
                if (creature.Alive)
                {
                    creature.Update(deltaTime, this);
                }
                else
                {
                    auukiCreatures.RemoveAt(i);
                    i--;
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

        /// <summary>
        /// Destroys any Auuki tiles underneath a player unit
        /// </summary>
        /// <param name="playerUnit">The unit to destroy tiles under</param>
        public void DestroyTilesUnderUnit(Unit playerUnit)
        {
            Point topLeft = new Point(
                 (int)Math.Floor(playerUnit.Position.X),
                 (int)Math.Floor(playerUnit.Position.Y)
                 );
            Point bottomRight = new Point(
             (int)Math.Ceiling(playerUnit.Position.X + playerUnit.Size.X),
             (int)Math.Ceiling(playerUnit.Position.Y + playerUnit.Size.Y)
                );
            //if any section is on an auukitile, remove the auukitile
            for (int i = topLeft.X; i < bottomRight.X; i++)
            {
                for (int j = topLeft.Y; j < bottomRight.Y; j++)
                {
                    if (tiles[i, j].HasAuukiTile)
                    {
                        tiles[i, j].RemoveAuuki();
                    }
                }
            }
        }

        /// <summary>
        /// Spawns a unit at the player base position
        /// </summary>
        public void SpawnUnit()
        {
            UnitTemplate newUnit = unitEditor.GetUnit();
            if (newUnit != null && resources.GreaterThan(newUnit.MaterialCost))
            {
                // newUnit is actually a unit template, a real position need to be given
                Unit spawnableUnit = new Unit(newUnit, playerBase.CenterPosition);
                spawnableUnit.Goal = playerBase.SpawnPosition;
                if (Collides(spawnableUnit))
                {

                        playerUnitQueue.Enqueue(spawnableUnit);
                }
                else
                {
                    playerUnits.Add(spawnableUnit);
                }
                resources.Remove(newUnit.MaterialCost);
            }
        }

        /// <summary>
        /// Checks if a given creature is colliding
        /// </summary>
        /// <param name="creature">The creature to check</param>
        /// <returns>If the creature is colliding</returns>
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

        /// <summary>
        /// Checks if a player unit is colliding with anything it shouldn't be
        /// </summary>
        /// <param name="playerUnit">The player's unit</param>
        /// <returns>If a player unit is colliding with anything it shouldn't be</returns>
        public bool Collides(Unit playerUnit)
        {
            bool colliding = false;
            Point topLeft = new Point(
                 (int)Math.Floor(playerUnit.Position.X),
                 (int)Math.Floor(playerUnit.Position.Y)
                 );
            Point bottomRight = new Point(
             (int)Math.Ceiling(playerUnit.Position.X + playerUnit.Size.X),
             (int)Math.Ceiling(playerUnit.Position.Y + playerUnit.Size.Y)
                );
            //if any bit is out of bounds, it collides
            if (playerUnit.Position.X < 0
                || playerUnit.Position.Y < 0
                || playerUnit.Position.X + playerUnit.Size.X > tiles.GetLength(0)
                || playerUnit.Position.Y + playerUnit.Size.Y > tiles.GetLength(1))
            {
                colliding = true;
                goto Collided;
            }

            //if any section is on a solid tile, it collides
            for (int i = topLeft.X; i < bottomRight.X; i++)
            {
                for (int j = topLeft.Y; j < bottomRight.Y; j++)
                {
                    if (tiles[i, j].IsSolid || tiles[i, j].HasAuukiStructure)
                    {
                        colliding = true;
                        goto Collided;
                        //goto is only valid way to exit nested loops, even though it's considered "weird"
                    }
                }
            }
            for (int i = 0; i < playerUnits.Count; i++)
            {
                if (playerUnits[i] != playerUnit && playerUnit.Collides(playerUnits[i]))
                {
                    colliding = true;
                    goto Collided;
                }
            }
        Collided:
            return colliding;
        }

        /// <summary>
        /// Exits the unit editor
        /// </summary>
        public void ExitUnitEditor()
        {
            unitEditor.Enabled = false;
        }

        /// <summary>
        /// If the level is over, and the player lost
        /// </summary>
        public bool Lost
        {
            get
            {
                return !playerBase.Alive;
            }
        }
        /// <summary>
        /// If the level is over, and the player won
        /// </summary>
        public bool Won
        {
            get
            { 
                if(auukiCreatures.Count != 0)
                {
                    return false;
                }
                for(int i  = 0;i < tiles.GetLength(0); i++)
                {
                    for(int j = 0;j < tiles.GetLength(1); j++)
                    {
                        if (tiles[i,j].HasAuukiTile)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}