using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    internal class WaterTile : Tile
    {
        public WaterTile(Point point) : base(Assets.GetTexture("water"), point, 0.001f)
        {

        }
        public override void InfectWithAuuki()
        {
            AuukiTile = new ShallowWaterAuuki(GetPosition());
        }
    }
}
