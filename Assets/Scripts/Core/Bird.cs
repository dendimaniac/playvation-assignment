using System.Collections;
using UnityEngine;

namespace Core
{
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

            StopAllCoroutines();
            StartCoroutine(ImmuneCountdown(maxImmuneDuration));
        }

        private void OnImmuneFading()
        {
            if (_anim.GetBool(ImmuneFading)) return;

            _anim.SetBool(Immune, false);
            _anim.SetBool(ImmuneFading, true);
        }

        private void OnImmuneEnded()
        {
            _birdController.IsImmune = false;
            _anim.SetBool(Immune, false);
            _anim.SetBool(ImmuneFading, false);
        }

        private void Jump()
        {
            _anim.SetTrigger(Flap);
            _birdController.Jump();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_birdController.IsImmune && other.gameObject.GetComponent<Column>()) return;

            _birdController.BirdDied();
            _anim.SetTrigger(Die);
            gameControl.BirdDied();
        }

        private IEnumerator ImmuneCountdown(float maxImmuneDuration)
        {
            var currentTimer = maxImmuneDuration;
            while (currentTimer > 0f)
            {
                currentTimer -= Time.deltaTime;
                if (currentTimer <= 1f)
                {
                    OnImmuneFading();
                }

                yield return null;
            }

            OnImmuneEnded();
        }

        private void OnDisable()
        {
            ImmunePowerUp.OnImmunePickedUp -= OnStartImmune;
        }
    }
}