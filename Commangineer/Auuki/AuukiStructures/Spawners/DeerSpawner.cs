using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    public class DeerSpawner : Spawner
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