using System;
using Flappy_Bird_Style.Scripts;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce = 200;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameControl gameControl;

    private Animator _anim;
    private BirdController _birdController;

    private static readonly int Flap = Animator.StringToHash("Flap");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Immune = Animator.StringToHash("Immune");
    private static readonly int ImmuneFading = Animator.StringToHash("ImmuneFading");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        var rb2d = GetComponent<Rigidbody2D>();
        var topBorder = gameCamera.orthographicSize + gameCamera.transform.position.y -
                        GetComponent<SpriteRenderer>().bounds.size.y;
        _birdController = new BirdController(upForce, rb2d, topBorder);
        ImmunePowerUp.OnImmunePickedUp += OnStartImmune;
    }

    private void Update()
    {
        if (_birdController.IsDead) return;

        _birdController.CheckResetMovement();

        if (!Input.GetMouseButtonDown(0)) return;

        Jump();
    }

    private void OnStartImmune(float maxImmuneDuration)
    {
        _birdController.IsImmune = true;

        _anim.SetBool(Immune, true);
        _anim.SetBool(ImmuneFading, false);
        _birdController.ImmuneFading += OnImmuneFading;
        _birdController.ImmuneEnded += OnImmuneEnded;
        StartCoroutine(_birdController.ImmuneCountdown(5f));
    }

    private void OnImmuneFading()
    {
        _anim.SetBool(Immune, false);
        _anim.SetBool(ImmuneFading, true);
        _birdController.ImmuneFading -= OnImmuneFading;
    }

    private void OnImmuneEnded()
    {
        _birdController.IsImmune = false;
        _anim.SetBool(Immune, false);
        _anim.SetBool(ImmuneFading, false);
        _birdController.ImmuneEnded -= OnImmuneEnded;
    }

    private void Jump()
    {
        _anim.SetTrigger(Flap);
        _birdController.Jump();
    }

    private void OnCollisionEnter2D()
    {
        if (_birdController.IsImmune) return;

        _birdController.BirdDied();
        _anim.SetTrigger(Die);
        gameControl.BirdDied();
    }

    private void OnDisable()
    {
        _birdController.ImmuneFading -= OnImmuneFading;
        _birdController.ImmuneEnded -= OnImmuneEnded;
        ImmunePowerUp.OnImmunePickedUp += OnStartImmune;
    }
}