using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    internal class DirtTile : Tile
    {
        public DirtTile(Point point) : base(Assets.GetTexture("dirt"), point, 0.005f)
        {

        }
        public override void InfectWithAuuki()
        {
            AuukiTile = new WoodlandFloorAuuki(Position);
        }

        //infectWithAuuki 
    }
}
