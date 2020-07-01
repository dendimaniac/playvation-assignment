using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Flappy_Bird_Style.Scripts
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
            _timeSinceLastSpawned = 0f;

            _powerUps = new GameObject[powerUpPoolSize];
            for (var i = 0; i < powerUpPoolSize; i++)
            {
                _powerUps[i] = Instantiate(powerUpPrefab, _objectPoolPosition, Quaternion.identity);
            }
        }

        private void Update()
        {
            _timeSinceLastSpawned += Time.deltaTime;
        }

        public void CheckSpawnPowerUp(Vector2 spawnPosition)
        {
            if (_timeSinceLastSpawned < spawnRate) return;

            _timeSinceLastSpawned = 0f;

            if (Random.Range(0f, 1f) > spawnChance) return;

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