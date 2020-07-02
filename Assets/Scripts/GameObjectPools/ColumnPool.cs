using Core;
using UnityEngine;

namespace GameObjectPools
{
    public class ColumnPool : BasePool
    {
        [SerializeField] private GameControl gameControl;
        [SerializeField] private ImmunePowerUpPool immunePowerUpPool;
        [SerializeField] private float columnMin = -1f;
        [SerializeField] private float columnMax = 3.5f;

        private const float SpawnXPosition = 10f;


        protected override void Update()
        {
            base.Update();

            if (gameControl.GameOver || !(TimeSinceLastSpawned >= spawnRate)) return;

            Spawn();
        }

        private void Spawn()
        {
            TimeSinceLastSpawned = 0f;

            var spawnYPosition = Random.Range(columnMin, columnMax);
            var spawnPosition = new Vector2(SpawnXPosition, spawnYPosition);

            immunePowerUpPool.CheckSpawnPowerUp(spawnPosition);
            MoveObjectToNewPosition(spawnPosition);

            IncreaseCurrentSpawnIndex();
        }
    }
}