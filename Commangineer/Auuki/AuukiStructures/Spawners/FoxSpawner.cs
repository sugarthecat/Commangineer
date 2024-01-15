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
    /// Creates a new Auuki fox spawner
    /// </summary>
    internal class FoxSpawner : Spawner
    {
        // Creates a new spawner of it's type
        public FoxSpawner(Point spawnPosition) : base(spawnPosition, new Point(3, 3), 2, 180, 5)
        {

        }
        /// <summary>
        /// Creates a new Auuki fox
        /// </summary>
        /// <returns>A Auuki fox</returns>
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}
