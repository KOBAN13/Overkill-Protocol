using StrategyInstaller;
using UnityEngine;

namespace Input.Interface
{
    public interface IInputSystem : IStrategy
    {
        Vector2 Input { get; }
        Vector3 PositionInMouseClick { get; }
    }
}