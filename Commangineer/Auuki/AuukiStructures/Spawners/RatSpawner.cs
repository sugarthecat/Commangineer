using Commangineer.Auuki.AuukiCreatures;
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
    /// A Auuki rat spawner
    /// </summary>
    internal class RatSpawner : Spawner
    {
        // Creates a new spawner of it's type
        public RatSpawner(Point spawnPosition) : base(spawnPosition, new Point(1, 1), 0,5,  5)
        {

        }
        /// <summary>
        /// Creates a new Auuki rat
        /// </summary>
        /// <returns>A new Auuki rat</returns>
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new Rat(SpawnPosition);
        }
    }
}
