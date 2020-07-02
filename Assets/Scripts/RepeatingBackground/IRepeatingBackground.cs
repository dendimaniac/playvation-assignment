using UnityEngine;

namespace RepeatingBackground
{
    public interface IRepeatingBackground
    {
        Vector3 Position { get; set; }
        float GroundHorizontalLength { get; }
    }
}