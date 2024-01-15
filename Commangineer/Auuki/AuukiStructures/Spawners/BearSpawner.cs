using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    public class BearSpawner : Spawner
    {
        public BearSpawner(Point spawnPosition) : base(spawnPosition, new Point(4, 4), 2, 300, 5)
        {
        }

        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}