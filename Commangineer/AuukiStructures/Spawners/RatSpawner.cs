using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiStructures.Spawners
{
    internal class RatSpawner : Spawner
    {
        public RatSpawner(Point spawnPosition) : base(spawnPosition, new Point(1,1), 1, 60)
        {

        }
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}
