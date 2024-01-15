using Commangineer.Auuki;
using Microsoft.Xna.Framework;

namespace Commangineer.Floor_Auuki_types
{
    public class ShallowWaterAuuki : AuukiFloor

    {
        public ShallowWaterAuuki(Point position)
            : base(
                new FloorAuukiStage[] {
                    new FloorAuukiStage(30,Assets.GetTexture("algae"),100f), // Weeds
                    new FloorAuukiStage(100,Assets.GetTexture("lilypads"),200f)
                }, position)
        {
        }
    }
}