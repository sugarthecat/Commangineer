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

    internal class GameEvent
    {
        private bool activated = false;
        private bool active = false;
        private GameValue gameValue;
        private ValueRelationship comparison;
        private int threshold;

        public GameEvent(JsonObject actionJSON)
        {
            gameValue = GameValue.GameTime;
            comparison = ValueRelationship.LessThan;
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