using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        if (!gameControl)
        {
            gameControl = FindObjectOfType<GameControl>();
        }

        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody2D.velocity = new Vector2(gameControl.ScrollSpeed, 0);
    }

    private void Update()
    {
        if (gameControl.GameOver)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}