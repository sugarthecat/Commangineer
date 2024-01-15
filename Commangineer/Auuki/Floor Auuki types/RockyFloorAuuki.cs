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
                    new FloorAuukiStage(5,Assets.GetTexture("weeds"),10f), // Weeds
                    new FloorAuukiStage(10,Assets.GetTexture("vines"),100f), // Grass
                    new FloorAuukiStage(70,Assets.GetTexture("flowervines"),200f)
                }, position)
        {
        }
    }
}