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
        public BearSpawner(Point spawnPosition) : base(spawnPosition, new Point(4, 4), 2, 300)
        {

        }
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}
