using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    /// <summary>
    /// A Auuki fox spawner
    /// </summary>
    public class FoxSpawner : Spawner
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