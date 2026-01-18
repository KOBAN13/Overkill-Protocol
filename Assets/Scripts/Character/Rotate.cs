using System;
using System.Threading;
using Character.Config;
using Character.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Character
{
    public class Rotate : IRotate, IDisposable
    {
        private readonly PlayerComponents _playerComponents;
        private readonly IPlayerParameters _playerParameters;
        private CancellationTokenSource _cancellationTokenSource;
        
        public Rotate(PlayerComponents playerComponents, IPlayerParameters playerParameters)
        {
            _playerComponents = playerComponents;
            _playerParameters = playerParameters;
        }

        public async UniTask RotateCharacter(Vector3 mousePosition)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            var ray = _playerComponents.Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hitGround, _playerParameters.MaxDistance, _playerParameters.CollisionLayerMask))
            {
                await FindDirection(hitGround);
            }
        }

        private async UniTask FindDirection(RaycastHit hit)
        {
            var direction = hit.point - _playerComponents.PlayerTransform.position;
                
            try
            {
                await Lerp(-direction);
            }
            catch (OperationCanceledException cancel)
            {
                Debug.LogWarning($"Operation is cancelled {cancel.Message}");
            }
        }

        private async UniTask Lerp(Vector3 direction)
        {
            direction.y = 0f;

            var angle = Quaternion.Angle(_playerComponents.PlayerTransform.rotation,
                Quaternion.LookRotation(direction));

            var rotationSpeed = _playerParameters.RotationSpeed;
            var rotationStep = rotationSpeed * Time.deltaTime;

            while (angle > rotationStep)
            {
                _playerComponents.PlayerTransform.rotation = Quaternion.Slerp(
                    _playerComponents.PlayerTransform.rotation, Quaternion.LookRotation(direction),
                    rotationStep / angle);

                angle -= rotationStep;

                await UniTask.Yield(_cancellationTokenSource.Token);
            }

            await UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}