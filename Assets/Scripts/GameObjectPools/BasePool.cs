using UnityEngine;

namespace GameObjectPools
{
    public class BasePool : MonoBehaviour
    {
        [SerializeField] protected GameObject prefab;
        [SerializeField] protected int prefabPoolSize = 3;
        [SerializeField] protected float spawnRate = 3f;

        private GameObject[] _spawnedArray;
        private int _currentSpawnIndex;

        private readonly Vector2 _objectPoolPosition = new Vector2(-15, -35);
        protected float TimeSinceLastSpawned;

        protected virtual void Awake()
        {
            TimeSinceLastSpawned = spawnRate;

            _spawnedArray = new GameObject[prefabPoolSize];
            for (var i = 0; i < prefabPoolSize; i++)
            {
                _spawnedArray[i] = Instantiate(prefab, _objectPoolPosition, Quaternion.identity);
                _spawnedArray[i].SetActive(false);
            }
        }

        protected virtual void Update()
        {
            TimeSinceLastSpawned += Time.deltaTime;
        }

        protected void MoveObjectToNewPosition(Vector2 spawnPosition)
        {
            _spawnedArray[_currentSpawnIndex].SetActive(true);
            _spawnedArray[_currentSpawnIndex].transform.position = spawnPosition;
        }

        protected void IncreaseCurrentSpawnIndex()
        {
            _currentSpawnIndex++;

            if (_currentSpawnIndex >= prefabPoolSize)
            {
                _currentSpawnIndex = 0;
            }
        }
    }
}