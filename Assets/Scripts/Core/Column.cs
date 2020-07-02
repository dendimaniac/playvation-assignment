using UnityEngine;

namespace Core
{
    public class Column : MonoBehaviour
    {
        private static GameControl _gameControl;

        private void Awake()
        {
            if (!_gameControl)
            {
                _gameControl = FindObjectOfType<GameControl>();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Bird>() != null)
            {
                _gameControl.BirdScored();
            }
        }
    }
}