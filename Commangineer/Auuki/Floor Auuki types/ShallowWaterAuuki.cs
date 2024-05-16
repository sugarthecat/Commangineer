using Commangineer.Auuki;
using Microsoft.Xna.Framework;

namespace Commangineer.Floor_Auuki_types
{
    /// <summary>
    /// A shallow water Auuki
    /// </summary>
    public class ShallowWaterAuuki : AuukiFloor
    {
        // Creates a new Auuki Floor of its type
        public ShallowWaterAuuki(Point position)
            : base(
                new FloorAuukiStage[] {
                    new FloorAuukiStage(10,Assets.GetTexture("algae"),100f), // Weeds
                    new FloorAuukiStage(20,Assets.GetTexture("lilypads"),200f)
                }, position)
        {
        }
    }
}