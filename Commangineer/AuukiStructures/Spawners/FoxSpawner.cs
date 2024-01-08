using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiStructures.Spawners
{
    internal class FoxSpawner : Spawner
    {
        public FoxSpawner(Point spawnPosition) : base(spawnPosition, new Point(3, 3), 2, 180)
        {

        }
        protected override AuukiCreature CreateNewAnimal()
        {
            return new AuukiCreature();
        }
    }
}
