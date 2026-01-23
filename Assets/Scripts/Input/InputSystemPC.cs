using System;
using Input.Interface;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class InputSystemPC : IInputSystem, IInitializable, ITickable, IDisposable
    {
        public Vector2 MoveInput { get; private set; }
        public Vector3 PositionInMouseClick { get; private set; }
        public Observable<Unit> OnClick => _onClick;
        
        private readonly Subject<Unit> _onClick = new();
        private readonly NewInputSystem _input;

        public InputSystemPC(NewInputSystem input)
        {
            _input = input;
        }

        private void GetMovement()
        {
            MoveInput = _input.Move.MoveWithWASD.ReadValue<Vector2>();
        }
        
        public void Tick()
        {
            GetMovement();
        }

        public void Initialize()
        {
            _input.asset.bindingMask = InputBinding.MaskByGroup("PC");
            _input.Enable();
            _input.Mouse.Fire.performed += OnFire;
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
            var mouse = Mouse.current;
            
            PositionInMouseClick = mouse.position.ReadValue();
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
