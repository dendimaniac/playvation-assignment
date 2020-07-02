using System.Collections;
using Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Playmode_Tests
{
    public class BirdTest
    {
        private GameObject _birdGameObject;
        private Rigidbody2D _birdRigidbody2D;

        [SetUp]
        public void SetUp()
        {
            _birdGameObject = new GameObject();
            _birdRigidbody2D = _birdGameObject.AddComponent<Rigidbody2D>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_birdGameObject);
        }

        [UnityTest]
        public IEnumerator BirdPosition_ShouldNotBypassTopBorder()
        {
            var birdController = new BirdController(200f, _birdRigidbody2D, 5f);
            yield return JumpBird(birdController, 10);
            Assert.GreaterOrEqual(5f, _birdRigidbody2D.transform.position.y);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BirdIsDead_WhenBirdDiedCalled()
        {
            var birdController = new BirdController(200f, _birdRigidbody2D, 5f);
            birdController.BirdDied();
            Assert.True(birdController.IsDead);
            yield return null;
        }

        private IEnumerator JumpBird(BirdController birdController, int times)
        {
            for (var i = 0; i < times; i++)
            {
                birdController.Jump();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}