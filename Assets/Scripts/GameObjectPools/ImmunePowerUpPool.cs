using UnityEngine;

namespace GameObjectPools
{
    public class ImmunePowerUpPool : MonoBehaviour
    {
        [SerializeField] private GameObject powerUpPrefab;
        [SerializeField] private int powerUpPoolSize = 3;
        [SerializeField] private float spawnRate = 3f;
        [SerializeField] [Range(0f, 1f)] private float spawnChance;

        private GameObject[] _powerUps;
        private int _currentPowerUp;

        private readonly Vector2 _objectPoolPosition = new Vector2(-15, -35);
        private float _timeSinceLastSpawned;

        private void Awake()
        {
            _timeSinceLastSpawned = spawnRate;

            _powerUps = new GameObject[powerUpPoolSize];
            for (var i = 0; i < powerUpPoolSize; i++)
            {
                _powerUps[i] = Instantiate(powerUpPrefab, _objectPoolPosition, Quaternion.identity);
                _powerUps[i].SetActive(false);
            }
        }

        private void Update()
        {
            _timeSinceLastSpawned += Time.deltaTime;
        }

        public void CheckSpawnPowerUp(Vector2 spawnPosition)
        {
            if (_timeSinceLastSpawned < spawnRate) return;

            var chance = Random.Range(0f, 1f);
            if (chance > spawnChance) return;

            _timeSinceLastSpawned = 0f;

            _powerUps[_currentPowerUp].SetActive(true);
            _powerUps[_currentPowerUp].transform.position = spawnPosition;

            _currentPowerUp++;

            if (_currentPowerUp >= powerUpPoolSize)
            {
                _currentPowerUp = 0;
            }
        }
    }
}