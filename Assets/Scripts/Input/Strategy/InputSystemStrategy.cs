using System;
using UnityEngine;
using Zenject;

namespace Input.Strategy
{
    public class InputSystemStrategy : IInputSystem, IInitializable, ITickable, IDisposable
    {
        private readonly InputSystemPC _pcInput;
        private readonly MobileInputSystem _mobileInput;
        private IInputSystem _active;

        public InputSystemStrategy(InputSystemPC pcInput, MobileInputSystem mobileInput)
        {
            _pcInput = pcInput;
            _mobileInput = mobileInput;
        }

        public Vector2 Input => _active.Input;
        public Vector3 PositionInMouseClick => _active.PositionInMouseClick;

        public void Initialize()
        {
            _active = Application.isMobilePlatform 
                ? _mobileInput
                : _pcInput;

            if (_active is IInitializable initializable)
            {
                initializable.Initialize();
            }
        }

        public void Tick()
        {
            if (_active is ITickable tickable)
            {
                tickable.Tick();
            }
        }

        public void Dispose()
        {
            if (_active is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
