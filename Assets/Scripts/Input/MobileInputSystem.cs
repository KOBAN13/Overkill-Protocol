using System;
using Input.Interface;
using StrategyInstaller;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class MobileInputSystem : IInputSystem, IInitializable, ITickable, IDisposable
    {
        public Vector2 Input { get; private set; }
        public Vector3 PositionInMouseClick { get; private set; }

        private readonly NewInputSystem _input;

        public MobileInputSystem(NewInputSystem input)
        {
            _input = input;
        }

        public void Initialize()
        {
            _input.asset.bindingMask = InputBinding.MaskByGroup("MobileInputSystem");
            _input.Enable();
            _input.Mouse.Fire.performed += OnFire;
        }

        public void Tick()
        {
            Input = _input.Move.MoveWithWASD.ReadValue<Vector2>();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            var touch = Touchscreen.current.primaryTouch;
            
            PositionInMouseClick = touch.position.ReadValue();
        }

        public void Dispose()
        {
            _input.Mouse.Fire.performed -= OnFire;
            _input.Disable();
            _input.Dispose();
        }
    }
}
