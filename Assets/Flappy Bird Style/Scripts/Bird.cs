using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameControl gameControl;

    private bool isDead;
    private Animator anim;
    private Rigidbody2D rb2d;
    private float topBorder;

    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        topBorder = gameCamera.orthographicSize + gameCamera.transform.position.y -
                    GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        if (isDead) return;

        if (!Input.GetMouseButtonDown(0)) return;

        Jump();
    }

    private void Jump()
    {
        anim.SetTrigger(Flap);
        rb2d.velocity = Vector2.zero;

        if (transform.position.y >= topBorder) return;

        rb2d.AddForce(new Vector2(0, upForce));
    }

    private void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger(Die);
        gameControl.BirdDied();
    }
}