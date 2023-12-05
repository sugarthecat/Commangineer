using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    internal class DeepWaterTile : Tile
    {
        public DeepWaterTile(Point point) : base(Assets.GetTexture("deepwater"), point, 0f)
        {

        }
        public override void InfectWithAuuki()
        {
        }
    }
}
