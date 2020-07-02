using DatabaseHelper;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class GameControl : MonoBehaviour
    {
        [SerializeField] private DynamoDB dynamoDb;
        [Space]
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject gameOverText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [Space]
        [SerializeField] private float scrollSpeed = -1.5f;

        public float ScrollSpeed => scrollSpeed;
        public bool GameOver => _gameOver;

        private int HighScore
        {
            set
            {
                if (value <= _highScore) return;

                _highScore = value;
                highScoreText.text = $"High score: {value}";
            }
        }
        private int _highScore;
        private bool _gameOver;
        private int _score;
        private const string HighScoreKey = "HighScore";

        private void Awake()
        {
            dynamoDb.OnHighScoreUpdated += UpdateHighScore;

            HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        }

        private void UpdateHighScore(int newHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, newHighScore);
            HighScore = newHighScore;
        }

        private void Update()
        {
            if (_gameOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void BirdScored()
        {
            if (_gameOver) return;

            _score++;
            scoreText.text = "Score: " + _score;
        }

        public void BirdDied()
        {
            gameOverText.SetActive(true);
            _gameOver = true;
            dynamoDb.PostHighscore("Quan Dao", _score);
        }

        private void OnDisable()
        {
            dynamoDb.OnHighScoreUpdated -= UpdateHighScore;
        }
    }
}