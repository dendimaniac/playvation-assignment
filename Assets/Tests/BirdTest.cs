using System.Collections;
using System.Collections.Generic;
using Flappy_Bird_Style.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BirdTest
    {
        private GameObject birdGameObject;
        private Rigidbody2D birdRigidbody2D;

        [SetUp]
        public void SetUp()
        {
            birdGameObject = new GameObject();
            birdRigidbody2D = birdGameObject.AddComponent<Rigidbody2D>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(birdGameObject);
        }

        [UnityTest]
        public IEnumerator BirdTestWithEnumeratorPasses()
        {
            var birdController = new BirdController(200f, birdRigidbody2D, 5f);
            yield return JumpBird(birdController, 10);
            Assert.GreaterOrEqual(5f, birdRigidbody2D.transform.position.y);
            yield return null;
        }

        private IEnumerator JumpBird(BirdController birdController, int times)
        {
            for (var i = 0; i < times; i++)
            {
                birdController.Jump();
                yield return new WaitForSeconds(0.3f);
                Debug.Log(birdRigidbody2D.transform.position.y);
            }
        }
    }
}