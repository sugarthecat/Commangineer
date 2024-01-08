using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.AuukiStructures
{
    internal class Spawner : AuukiStructure
    {
        float animalSpawnTime = 0;
        float animalSpawnProgress = 0;
        public Spawner(Point spawnPosition, Point size, int minFloorTier, float animalSpawnTime) : base(spawnPosition, size, Assets.GetTexture("default"), minFloorTier)
        {

        }
        public new void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (animalSpawnProgress <= animalSpawnTime)
            {
                animalSpawnProgress += deltaTime;
            }
        }
        public bool CanSpawnAnimal
        {
            get
            {
                return animalSpawnProgress > animalSpawnTime;
            }
        }
        public AuukiCreature Animal
        {
            get
            {
                return CreateNewAnimal();
            }
        }
        protected virtual AuukiCreature CreateNewAnimal()
        {
            return new AuukiCreature();
        }
    }
}
