using Commangineer.Auuki.AuukiStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    /// <summary>
    /// A Auuki deer spawner
    /// </summary>
    internal class DeerSpawner : Spawner
    {
        // Creates a new spawner of it's type
        public DeerSpawner(Point spawnPosition) : base(spawnPosition, new Point(2, 2), 1, 120, 5)
        {

        }
        /// <summary>
        /// Creates a new Auuki deer
        /// </summary>
        /// <returns>A new Auuki deer</returns>
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}
