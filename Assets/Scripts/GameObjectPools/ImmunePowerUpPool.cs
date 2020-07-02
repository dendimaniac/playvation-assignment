using UnityEngine;

namespace GameObjectPools
{
    public class ImmunePowerUpPool : BasePool
    {
        [SerializeField] [Range(0f, 1f)] private float spawnChance;

        public void CheckSpawnPowerUp(Vector2 spawnPosition)
        {
            if (TimeSinceLastSpawned < spawnRate) return;

            var chance = Random.Range(0f, 1f);
            if (chance > spawnChance) return;

            TimeSinceLastSpawned = 0f;

            MoveObjectToNewPosition(spawnPosition);

            IncreaseCurrentSpawnIndex();
        }
    }
}