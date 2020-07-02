using Core;
using UnityEngine;

namespace GameObjectPools
{
    public class ColumnPool : MonoBehaviour
    {
        [SerializeField] private GameControl gameControl;
        [SerializeField] private ImmunePowerUpPool immunePowerUpPool;
        [Space]
        [SerializeField] private GameObject columnPrefab;
        [SerializeField] private int columnPoolSize = 5;
        [SerializeField] private float spawnRate = 3f;
        [SerializeField] private float columnMin = -1f;
        [SerializeField] private float columnMax = 3.5f;

        private GameObject[] _columns;
        private int _currentColumn;

        private readonly Vector2 _objectPoolPosition = new Vector2(-15, -25);
        private const float SpawnXPosition = 10f;
        private float _timeSinceLastSpawned;

        private void Awake()
        {
            _timeSinceLastSpawned = 0f;

            _columns = new GameObject[columnPoolSize];
            for (var i = 0; i < columnPoolSize; i++)
            {
                _columns[i] = Instantiate(columnPrefab, _objectPoolPosition, Quaternion.identity);
            }
        }


        private void Update()
        {
            _timeSinceLastSpawned += Time.deltaTime;

            if (gameControl.GameOver || !(_timeSinceLastSpawned >= spawnRate)) return;

            _timeSinceLastSpawned = 0f;

            float spawnYPosition = Random.Range(columnMin, columnMax);

            var spawnPosition = new Vector2(SpawnXPosition, spawnYPosition);

            immunePowerUpPool.CheckSpawnPowerUp(spawnPosition);
            _columns[_currentColumn].transform.position = spawnPosition;

            _currentColumn++;

            if (_currentColumn >= columnPoolSize)
            {
                _currentColumn = 0;
            }
        }
    }
}