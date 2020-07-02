using UnityEngine;

namespace GameObjectPools
{
    public class BasePool : MonoBehaviour
    {
        [SerializeField] protected GameObject prefab;
        [SerializeField] protected int prefabPoolSize = 3;
        [SerializeField] protected float spawnRate = 3f;

        protected GameObject[] SpawnedArray;
        protected int CurrentSpawnIndex;

        protected readonly Vector2 ObjectPoolPosition = new Vector2(-15, -35);
        protected float TimeSinceLastSpawned;

        private void Awake()
        {
            TimeSinceLastSpawned = spawnRate;

            SpawnedArray = new GameObject[prefabPoolSize];
            for (var i = 0; i < prefabPoolSize; i++)
            {
                SpawnedArray[i] = Instantiate(prefab, ObjectPoolPosition, Quaternion.identity);
                SpawnedArray[i].SetActive(false);
            }
        }

        protected virtual void Update()
        {
            TimeSinceLastSpawned += Time.deltaTime;
        }

        protected void MoveObjectToNewPosition(Vector2 spawnPosition)
        {
            SpawnedArray[CurrentSpawnIndex].SetActive(true);
            SpawnedArray[CurrentSpawnIndex].transform.position = spawnPosition;
        }

        protected void IncreaseCurrentSpawnIndex()
        {
            CurrentSpawnIndex++;

            if (CurrentSpawnIndex >= prefabPoolSize)
            {
                CurrentSpawnIndex = 0;
            }
        }
    }
}