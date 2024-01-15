using Commangineer.Auuki.AuukiCreatures;
using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    /// <summary>
    /// A Auuki deer spawner
    /// </summary>
    public class DeerSpawner : Spawner
    {
        // Creates a new spawner of it's type
        public DeerSpawner(Point spawnPosition) : base(spawnPosition, new Point(2, 2), 1, 40, 5)
        {
        }

        /// <summary>
        /// Creates a new Auuki deer
        /// </summary>
        /// <returns>A new Auuki deer</returns>
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new Deer(SpawnPosition);
        }
    }
}