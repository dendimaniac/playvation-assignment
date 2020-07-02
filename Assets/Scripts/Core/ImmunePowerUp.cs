using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public class ImmunePowerUp : MonoBehaviour
    {
        public static event Action<float> OnImmunePickedUp;

        [SerializeField] private float maxImmuneDuration = 2f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnImmunePickedUp?.Invoke(maxImmuneDuration);
            gameObject.SetActive(false);
        }
    }
}