using UnityEngine;

namespace RepeatingBackground
{
    public class RepeatingBackgroundController
    {
        private readonly IRepeatingBackground _repeatingBackground;

        public RepeatingBackgroundController(IRepeatingBackground repeatingBackground)
        {
            _repeatingBackground = repeatingBackground;
        }

        public void CheckRepositionBackground()
        {
            if (_repeatingBackground.Position.x < -_repeatingBackground.GroundHorizontalLength)
            {
                RepositionBackground();
            }
        }

        private void RepositionBackground()
        {
            var groundOffSet = new Vector2(_repeatingBackground.GroundHorizontalLength * 2f, 0);

            _repeatingBackground.Position = (Vector2) _repeatingBackground.Position + groundOffSet;
        }
    }
}