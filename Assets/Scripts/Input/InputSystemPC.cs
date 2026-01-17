using System;
using Input.Interface;
using R3;
using StrategyInstaller;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class InputSystemPC : IInputSystem, IInitializable, ITickable, IDisposable
    {
        public Vector2 Input { get; private set; }
        public Vector3 PositionInMouseClick { get; private set; }
        
        private readonly NewInputSystem _input;
        private readonly CompositeDisposable _compositeDisposable = new();

        public InputSystemPC(NewInputSystem input)
        {
            _input = input;
        }

        private void GetMovement()
        {
            Input = _input.Move.MoveWithWASD.ReadValue<Vector2>();
        }
        
        public void Tick()
        {
            GetMovement();
        }

        public void Initialize()
        {
            _input.asset.bindingMask = InputBinding.MaskByGroup("PCInputSystem");
            _input.Enable();
            _input.Mouse.Fire.performed += OnFire;
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
            var mouse = Mouse.current;
            if (mouse != null)
            {
                PositionInMouseClick = mouse.position.ReadValue();
            }
        }

        public void Dispose()
        {
            _input.Mouse.Fire.performed -= OnFire;
            _input.Disable();
            _input.Dispose();
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}
