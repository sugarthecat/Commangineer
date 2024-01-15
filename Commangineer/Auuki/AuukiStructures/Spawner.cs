using Microsoft.Xna.Framework;

namespace Commangineer.Auuki.AuukiStructures
{
    /// <summary>
    /// A Auuki Structure which can spawn Auuki Creatures
    /// </summary>
    public class Spawner : AuukiStructure
    {
        private float animalSpawnTime = 0;
        private float animalSpawnProgress = 0;

        // Creates a new spawner
        public Spawner(Point spawnPosition, Point size, int minFloorTier, float animalSpawnTime, int health) : base(spawnPosition, size, Assets.GetTexture("auukiDen"), minFloorTier, health)
        {
            this.animalSpawnTime = animalSpawnTime;
        }

        /// <summary>
        /// Updates the spawner
        /// </summary>
        /// <param name="deltaTime">The time since the last update</param>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (animalSpawnProgress <= animalSpawnTime)
            {
                animalSpawnProgress += deltaTime;
            }
        }

        /// <summary>
        /// Gets if the structure can spawn a creature
        /// </summary>
        public bool CanSpawnAnimal
        {
            get
            {
                return animalSpawnProgress > animalSpawnTime;
            }
        }

        /// <summary>
        /// Gets a new Auuki creature
        /// </summary>
        public AuukiCreature Animal
        {
            get
            {
                return CreateNewAnimal();
            }
        }

        /// <summary>
        /// Gets the spawner's spawn position
        /// </summary>
        protected Vector2 SpawnPosition
        {
            get
            {
                return new Vector2(Position.X + Size.X / 2f, Position.Y + Size.Y / 2f);
            }
        }

        /// <summary>
        /// Removes all progress towards spawning a creature
        /// </summary>
        protected void ClearSpawnProgress()
        {
            animalSpawnProgress = 0;
        }

        /// <summary>
        /// Creates a new Auuki creature
        /// </summary>
        /// <returns>A new Auuki creature at it's spawn position</returns>
        protected virtual AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}