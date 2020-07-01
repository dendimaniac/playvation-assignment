using System;
using System.Collections;
using UnityEngine;

namespace Flappy_Bird_Style.Scripts
{
    public class BirdController
    {
        private bool _isDead;
        private readonly float _upForce;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _topBorder;

        public bool IsImmune { get; set; }
        public bool IsDead => _isDead;

        public BirdController(float upForce, Rigidbody2D rigidbody2D, float topBorder)
        {
            _upForce = upForce;
            _rigidbody2D = rigidbody2D;
            _topBorder = topBorder;
        }

        public void CheckResetMovement()
        {
            if (_rigidbody2D.transform.position.y >= _topBorder)
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        public void Jump()
        {
            if (_rigidbody2D.transform.position.y >= _topBorder) return;

            _rigidbody2D.velocity = Vector2.zero;

            _rigidbody2D.AddForce(new Vector2(0, _upForce));
        }

        public void BirdDied()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _isDead = true;
        }
    }
}