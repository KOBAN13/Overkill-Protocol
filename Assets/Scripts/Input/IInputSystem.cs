using UnityEngine;

namespace Input
{
    public interface IInputSystem
    {
        Vector2 Input { get; }
        Vector3 PositionInMouseClick { get; }
    }
}