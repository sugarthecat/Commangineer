using Commangineer.Auuki.AuukiStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    internal class DeerSpawner : Spawner
    {
        public DeerSpawner(Point spawnPosition) : base(spawnPosition, new Point(2, 2), 1, 120, 5)
        {

        }
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}
