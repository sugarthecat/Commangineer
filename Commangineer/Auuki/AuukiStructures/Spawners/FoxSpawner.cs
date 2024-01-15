using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures.Spawners
{
    public class FoxSpawner : Spawner
    {
        public FoxSpawner(Point spawnPosition) : base(spawnPosition, new Point(3, 3), 2, 180, 5)
        {
        }

        protected override AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}