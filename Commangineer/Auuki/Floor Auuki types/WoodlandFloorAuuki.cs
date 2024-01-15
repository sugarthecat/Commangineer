﻿using Commangineer.Auuki;
using Microsoft.Xna.Framework;

namespace Commangineer.Floor_Auuki_types
{
    /// <summary>
    /// A woodland floor Auuki
    /// </summary>
    public class WoodlandFloorAuuki : AuukiFloor
    {
        // Creates a new Auuki Floor of its type
        public WoodlandFloorAuuki(Point position)
            : base(
                new FloorAuukiStage[] {
                    new FloorAuukiStage(30,Assets.GetTexture("weeds"),50f), // Weeds
                    new FloorAuukiStage(100,Assets.GetTexture("grass"),100f), // Grass
                    new FloorAuukiStage(300,Assets.GetTexture("leaves"),800f)
                }, position)
        {
        }
    }
}