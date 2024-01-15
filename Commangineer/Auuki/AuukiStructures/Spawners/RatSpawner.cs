using Commangineer.Auuki.AuukiCreatures;
using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    public class RatSpawner : Spawner
    {
        public RatSpawner(Point spawnPosition) : base(spawnPosition, new Point(1, 1), 0, 5, 5)
        {
        }

        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new Rat(SpawnPosition);
        }
    }
}