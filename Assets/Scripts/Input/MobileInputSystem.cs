using System;
using Input.Interface;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class MobileInputSystem : IInputSystem, IInitializable, ITickable, IDisposable
    {
        public Vector2 MoveInput { get; private set; }
        public Vector3 PositionInMouseClick { get; private set; }
        public Observable<Unit> OnClick => _onClick;
        
        private readonly Subject<Unit> _onClick = new();
        private readonly NewInputSystem _input;

        public MobileInputSystem(NewInputSystem input)
        {
            _input = input;
        }

        public void Initialize()
        {
            _input.asset.bindingMask = InputBinding.MaskByGroup("Mobile");
            _input.Enable();
            _input.Mouse.Fire.performed += OnFire;
        }

        public void Tick()
        {
            var touch = Touchscreen.current.primaryTouch;
            
            if (touch == null || !touch.press.isPressed)
            {
                MoveInput = Vector2.zero;
                return;
            }

            MoveInput = touch.delta.ReadValue() * 0.01f;
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            var touch = Touchscreen.current.primaryTouch;
            
            PositionInMouseClick = touch.position.ReadValue();
            _onClick.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _input.Mouse.Fire.performed -= OnFire;
            _input.Disable();
            _input.Dispose();
        }
    }
}
