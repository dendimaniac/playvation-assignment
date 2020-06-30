using Flappy_Bird_Style.Scripts;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameControl gameControl;

    private Animator _anim;
    private BirdController _birdController;

    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        var rb2d = GetComponent<Rigidbody2D>();
        var topBorder = gameCamera.orthographicSize + gameCamera.transform.position.y -
                        GetComponent<SpriteRenderer>().bounds.size.y;
        _birdController = new BirdController(upForce, rb2d, topBorder);
    }

    private void Update()
    {
        if (_birdController.IsDead) return;

        _birdController.CheckResetMovement();

        if (!Input.GetMouseButtonDown(0)) return;

        Jump();
    }

    private void Jump()
    {
        _anim.SetTrigger(Flap);
        _birdController.Jump();
    }

    private void OnCollisionEnter2D()
    {
        _birdController.BirdDied();
        _anim.SetTrigger(Die);
        gameControl.BirdDied();
    }
}