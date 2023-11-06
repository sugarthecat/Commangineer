using Microsoft.Xna.Framework;

namespace Commangineer.Floor_Auuki_types
{
    internal class WoodlandFloorAuuki : FloorAuuki
    {
        public WoodlandFloorAuuki(Point position)
            : base(
                new FloorAuukiStage[] {
                    new FloorAuukiStage(30,Assets.GetTexture("weeds"),50f), // Weeds
                    new FloorAuukiStage(100,Assets.GetTexture("grass"),100f), // Grass
                    new FloorAuukiStage(300,Assets.GetTexture("leaves"),800f), // Leaves
                    new FloorAuukiStage(300,Assets.GetTexture("wood")), // wood
                },position)
        {

        }
    }
}