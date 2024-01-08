using Commangineer.Floor_Auuki_types;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Tile_Types
{
    internal class MountainTile : Tile
    {
        public MountainTile(Point point) : base(Assets.GetTexture("mountain"), point, 0f)
        {

        }
    }
}
