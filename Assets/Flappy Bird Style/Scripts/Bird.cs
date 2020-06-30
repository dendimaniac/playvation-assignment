using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce; //Upward force of the "flap".
    [SerializeField] private Camera gameCamera;

    private bool isDead; //Has the player collided with a wall?
    private Animator anim; //Reference to the Animator component.
    private Rigidbody2D rb2d; //Holds a reference to the Rigidbody2D component of the bird.
    private Transform transform;
    private float topBorder;

    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");

    private void Start()
    {
        //Get reference to the Animator component attached to this GameObject.
        anim = GetComponent<Animator>();
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        topBorder = gameCamera.orthographicSize + gameCamera.transform.position.y -
                    GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        //Don't allow control if the bird has died.
        if (isDead) return;

        //Look for input to trigger a "flap".
        if (!Input.GetMouseButtonDown(0)) return;

        //...tell the animator about it and then...
        anim.SetTrigger(Flap);
        //...zero out the birds current y velocity before...
        rb2d.velocity = Vector2.zero;

        if (transform.position.y >= topBorder) return;

        //	new Vector2(rb2d.velocity.x, 0);
        //..giving the bird some upward force.
        rb2d.AddForce(new Vector2(0, upForce));
    }

    private void OnCollisionEnter2D()
    {
        // Zero out the bird's velocity
        rb2d.velocity = Vector2.zero;
        // If the bird collides with something set it to dead...
        isDead = true;
        //...tell the Animator about it...
        anim.SetTrigger(Die);
        //...and tell the game control about it.
        GameControl.instance.BirdDied();
    }
}