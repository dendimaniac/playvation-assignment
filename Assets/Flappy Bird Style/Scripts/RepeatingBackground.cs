using Flappy_Bird_Style.Scripts;
using Flappy_Bird_Style.Scripts.Interfaces;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour, IRepeatingBackground
{
    private RepeatingBackgroundController _controller;

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    public float GroundHorizontalLength { get; private set; }

    private void Awake()
    {
        var groundCollider = GetComponent<BoxCollider2D>();
        GroundHorizontalLength = groundCollider.size.x;
        _controller = new RepeatingBackgroundController(this);
    }

    private void Update()
    {
        _controller.CheckRepositionBackground();
    }
}