using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Nodes;

namespace Commangineer
{
    internal enum GameValue
    {
        GameTime,
        PlayerUnitCount,
        AuukiUnitCount
    }
    internal enum ValueRelationship
    {
        LessThan,
        GreaterThan    
    }
    internal enum EventType
    {
        dialogue
    }

    internal class GameAction
    {
        private bool activated = false;
        private bool active = false;
        private GameValue gameValue;
        private ValueRelationship comparison;
        private List<Dictionary<string,string>> eventList;
        private int threshold;

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
            foreach (JsonObject node in actionJSON["events"].AsArray())
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
            if(activated)
            {
                if ((comparison == ValueRelationship.LessThan && gameValue >= threshold) 
                    || (comparison == ValueRelationship.GreaterThan && gameValue <= threshold))
                {
                    activated = false;
                }
            }
            else if(!active) 
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