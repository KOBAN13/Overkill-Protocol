using R3;
using StrategyInstaller;
using UnityEngine;

namespace Input.Interface
{
    public interface IInputSystem : IStrategy
    {
        Vector2 MoveInput { get; }
        Vector3 PositionInMouseClick { get; }
        Observable<Unit> OnClick { get; }
    }
}