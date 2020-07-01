using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private float _groundHorizontalLength;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        var groundCollider = GetComponent<BoxCollider2D>();
        _groundHorizontalLength = groundCollider.size.x;
    }

    private void Update()
    {
        if (transform.position.x < -_groundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        var groundOffSet = new Vector2(_groundHorizontalLength * 2f, 0);

        _transform.position = (Vector2) _transform.position + groundOffSet;
    }
}