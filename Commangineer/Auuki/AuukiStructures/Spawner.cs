﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki.AuukiStructures
{
    internal class Spawner : AuukiStructure
    {
        float animalSpawnTime = 0;
        float animalSpawnProgress = 0;
        public Spawner(Point spawnPosition, Point size, int minFloorTier, float animalSpawnTime, int health) : base(spawnPosition, size, Assets.GetTexture("auukiDen"), minFloorTier, health)
        {
            this.animalSpawnTime = animalSpawnTime;
        }
        public override void Update(float deltaTime)
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
        protected Vector2 SpawnPosition
        {
            get
            {
                return new Vector2(Position.X + Size.X / 2f, Position.Y + Size.Y / 2f);
            }
        }
        protected void ClearSpawnProgress()
        {

            animalSpawnProgress = 0;
        }
        protected virtual AuukiCreature CreateNewAnimal()
        {
            ClearSpawnProgress();
            return new AuukiCreature(SpawnPosition);
        }
    }
}
