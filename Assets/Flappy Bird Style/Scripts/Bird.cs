using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce; //Upward force of the "flap".
    [SerializeField] private Camera gameCamera;

    private bool isDead; //Has the player collided with a wall?
    private Animator anim; //Reference to the Animator component.
    private Rigidbody2D rb2d; //Holds a reference to the Rigidbody2D component of the bird.
    private float topBorder;

    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");

    private void Start()
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
        GameControl.instance.BirdDied();
    }
}