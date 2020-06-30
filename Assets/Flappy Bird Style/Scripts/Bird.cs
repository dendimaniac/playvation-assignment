using Flappy_Bird_Style.Scripts;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameControl gameControl;

    private Animator anim;
    private BirdController birdController;

    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        var rb2d = GetComponent<Rigidbody2D>();
        var topBorder = gameCamera.orthographicSize + gameCamera.transform.position.y -
                        GetComponent<SpriteRenderer>().bounds.size.y;
        birdController = new BirdController(upForce, rb2d, topBorder);
    }

    private void Update()
    {
        if (birdController.IsDead) return;

        if (!Input.GetMouseButtonDown(0)) return;

        Jump();
    }

    private void Jump()
    {
        anim.SetTrigger(Flap);
        birdController.Jump();
    }

    private void OnCollisionEnter2D()
    {
        birdController.BirdDied();
        anim.SetTrigger(Die);
        gameControl.BirdDied();
    }
}