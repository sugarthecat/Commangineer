using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiStructures.Spawners
{
    internal class BearSpawner : Spawner
    {
        public BearSpawner(Point spawnPosition) : base(spawnPosition, new Point(4, 4), 3, 300)
        {

        }
        protected override AuukiCreature CreateNewAnimal()
        {
            return new AuukiCreature();
        }
    }
}
