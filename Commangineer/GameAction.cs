using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Commangineer
{
    public enum GameValue
    {
        GameTime,
        PlayerUnitCount,
        AuukiUnitCount
    }

    public enum ValueRelationship
    {
        LessThan,
        GreaterThan
    }

    /// <summary>
    /// A class representing a action event when in levels, which can be triggered by a variety of trackers and is used to trigger events such as dialogue popups
    /// </summary>
    public class GameAction
    {
        private bool activated = false;
        private bool active = false;
        private GameValue gameValue;
        private ValueRelationship comparison;
        private List<Dictionary<string, string>> eventList;
        private int threshold;

        /// <summary>
        /// Inlitializes the action
        /// </summary>
        /// <param name="actionJSON">A JSON containing details of the action</param>
        public GameAction(JsonObject actionJSON)
        {
            gameValue = GameValue.GameTime;
            comparison = ValueRelationship.LessThan;
            eventList = new List<Dictionary<string, string>>();
            switch ((string)actionJSON["gameValue"])
            {
                case "time":
                    gameValue = GameValue.GameTime;
                    break;

                case "units":
                    gameValue = GameValue.PlayerUnitCount;
                    break;

                case "auukiUnits":
                    gameValue = GameValue.AuukiUnitCount;
                    break;
            }
            switch ((string)actionJSON["comparator"])
            {
                case "<":
                    comparison = ValueRelationship.LessThan;
                    break;

                case ">":
                    comparison = ValueRelationship.GreaterThan;
                    break;
            }
            threshold = (int)actionJSON["compareValue"];
            // Loads in the action events that can get triggered
            foreach (JsonObject node in actionJSON["actions"].AsArray())
            {
                Dictionary<string, string> foundEvent = new Dictionary<string, string>();
                foreach (KeyValuePair<string, JsonNode> pair in node.AsObject())
                {
                    foundEvent.Add(pair.Key, (string)pair.Value);
                }
                eventList.Add(foundEvent);
            }
        }

        public GameValue GameValue
        {
            get
            {
                return gameValue;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
        }

        public List<Dictionary<string, string>> Events
        {
            get
            {
                return eventList;
            }
        }

        public void Update(int gameValue)
        {
            if (activated)
            {
                if ((comparison == ValueRelationship.LessThan && gameValue >= threshold)
                    || (comparison == ValueRelationship.GreaterThan && gameValue <= threshold))
                {
                    activated = false;
                }
            }
            else if (!active)
            {
                if ((comparison == ValueRelationship.LessThan && gameValue < threshold)
                    || (comparison == ValueRelationship.GreaterThan && gameValue > threshold))
                {
                    active = true;
                }
            }
        }

        public void Deactivate()
        {
            active = false;
            activated = true;
        }
    }
}