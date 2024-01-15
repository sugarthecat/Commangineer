using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    /// <summary>
    /// A Auuki bear spawner
    /// </summary>
    public class BearSpawner : Spawner
    {
        // Creates a new spawner of it's type
        public BearSpawner(Point spawnPosition) : base(spawnPosition, new Point(4, 4), 2, 180, 5)
        {
        }

        /// <summary>
        /// Creates a new Auuki bear
        /// </summary>
        /// <returns>A new Auuki bear</returns>
        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}