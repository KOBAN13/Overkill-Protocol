using System;
using UnityEngine;
using Zenject;

namespace Input
{
    public class MobileInputSystem : IInputSystem, IInitializable, ITickable, IDisposable
    {
        private const float DragRadiusPixels = 120f;

        public Vector2 Input { get; private set; }
        public Vector3 PositionInMouseClick { get; private set; }

        private int _activeFingerId = -1;
        private Vector2 _startTouch;

        public void Initialize()
        {
            Input = Vector2.zero;
            PositionInMouseClick = Vector3.zero;
        }

        public void Tick()
        {
            if (UnityEngine.Input.touchCount == 0)
            {
                Input = Vector2.zero;
                _activeFingerId = -1;
                return;
            }

            for (var i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                var touch = UnityEngine.Input.GetTouch(i);

                if (_activeFingerId == -1 && touch.phase == TouchPhase.Began)
                {
                    _activeFingerId = touch.fingerId;
                    _startTouch = touch.position;
                    PositionInMouseClick = touch.position;
                }

                if (touch.fingerId != _activeFingerId)
                {
                    continue;
                }

                if (touch.phase is TouchPhase.Ended or TouchPhase.Canceled)
                {
                    Input = Vector2.zero;
                    _activeFingerId = -1;
                    return;
                }

                var delta = (touch.position - _startTouch) / DragRadiusPixels;
                Input = Vector2.ClampMagnitude(delta, 1f);
                return;
            }
        }

        public void Dispose()
        {
            Input = Vector2.zero;
            _activeFingerId = -1;
        }
    }
}
