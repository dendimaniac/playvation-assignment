using UnityEngine;

namespace Flappy_Bird_Style.Scripts.Interfaces
{
    public interface IRepeatingBackground
    {
        Vector3 Position { get; set; }
        float GroundHorizontalLength { get; }
    }
}