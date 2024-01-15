using Commangineer.Auuki;
using Microsoft.Xna.Framework;

namespace Commangineer.Floor_Auuki_types
{
    /// <summary>
    /// A rocky floor Auuki
    /// </summary>
    public class RockyFloorAuuki : AuukiFloor
    {
        // Creates a new Auuki Floor of its type
        public RockyFloorAuuki(Point position)
            : base(
                new FloorAuukiStage[] {
                    new FloorAuukiStage(5,Assets.GetTexture("weeds"),50f), // Weeds
                    new FloorAuukiStage(25,Assets.GetTexture("grass"),100f), // Grass
                    new FloorAuukiStage(50,Assets.GetTexture("leaves"),800f)
                }, position)
        {
        }
    }
}