using NSubstitute;
using NUnit.Framework;
using RepeatingBackground;
using UnityEngine;

namespace Tests.Editmode_Tests
{
    public class RepeatingBackgroundTest
    {
        private IRepeatingBackground _repeatingBackground;
        private RepeatingBackgroundController _controller;

        [SetUp]
        public void SetUp()
        {
            _repeatingBackground = Substitute.For<IRepeatingBackground>();
            _repeatingBackground.GroundHorizontalLength.Returns(5f);
            _controller = new RepeatingBackgroundController(_repeatingBackground);
        }

        [Test]
        public void BackgroundResetToCorrectPosition_WhenPassGroundLength()
        {
            var position = new Vector3(-6f, 0f, 0f);
            _repeatingBackground.Position.Returns(position);

            _controller.CheckRepositionBackground();

            Assert.AreEqual(4, _repeatingBackground.Position.x);
        }

        [Test]
        public void BackgroundDontResetPosition_WhenNotPassGroundLength()
        {
            var position = new Vector3(-4f, 0f, 0f);
            _repeatingBackground.Position.Returns(position);

            _controller.CheckRepositionBackground();

            Assert.AreEqual(-4, _repeatingBackground.Position.x);
        }
    }
}