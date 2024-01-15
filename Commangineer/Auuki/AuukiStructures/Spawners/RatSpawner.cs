using Commangineer.Auuki.AuukiCreatures;
using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    /// <summary>
    /// A Auuki rat spawner
    /// </summary>
    public class RatSpawner : Spawner
    {
        // Creates a new spawner of it's type
        public RatSpawner(Point spawnPosition) : base(spawnPosition, new Point(1, 1), 0, 20, 5)
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