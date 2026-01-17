using System;
using R3;
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
            _input.Enable();
            _input.Mouse.Fire.performed += OnFire;
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
            PositionInMouseClick = UnityEngine.Input.mousePosition;
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
