using Microsoft.Xna.Framework;

namespace Commangineer.Floor_Auuki_types
{
    internal class WoodlandFloorAuuki : FloorAuuki
    {
        private FloorAuukiStage[] stages = new FloorAuukiStage[4];

        public WoodlandFloorAuuki(Point position)
            : base(
                new FloorAuukiStage[] {
                    new FloorAuukiStage(30,Assets.GetTexture("grassOverlay"),5f), // Weeds
                    new FloorAuukiStage(100,Assets.GetTexture("grassTemp"),5f), // Grass
                    new FloorAuukiStage(300,Assets.GetTexture("leaves"),5f), // Leaves
                    new FloorAuukiStage(300,Assets.GetTexture("wood"),5f), // wood
                },position)
        {

        }
    }
}