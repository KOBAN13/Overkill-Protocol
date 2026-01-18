using System;
using Enemy.Config;
using R3;
using UnityEngine;
using Zenject;

namespace Services.Spawners
{
    public class EnemySpawner : IInitializable, IDisposable
    {
        private readonly PointsCamera _pointsCamera;
        private readonly SignalDeclaration.Factory.Factory _factory;

        private readonly EnemyConfig _enemyConfig;
        private readonly CompositeDisposable _compositeDisposable = new();

        public EnemySpawner(PointsCamera pointsCamera, EnemyConfig enemyConfig)
        {
            _pointsCamera = pointsCamera;
            _factory = factory;
            _enemyConfig = enemyConfig;
        }

        public async void Initialize()
        {
            var timeToSpawn = 2f;
            Observable
                .Timer(TimeSpan.FromSeconds(timeToSpawn), TimeSpan.FromSeconds(timeToSpawn))
                .Subscribe(_ => { StartSpawn(); })
                .AddTo(_compositeDisposable);

            Observable
                .Timer(TimeSpan.FromSeconds(10f), TimeSpan.FromSeconds(10f))
                .Subscribe(_ => timeToSpawn = Mathf.Clamp(timeToSpawn - 0.1f, 0.5f, 2f))
                .AddTo(_compositeDisposable);
        }

        private void StartSpawn()
        {
            var enemy = _enemyConfig.EnemyPrefab;
            _pointsCamera.Invisible(out var position);
            _factory.CreateInitDiContainerAndInitializeEnemy(enemy, enemy, position, Quaternion.identity);
        }

        public void Dispose()
        {
            _compositeDisposable?.Clear();
            _compositeDisposable?.Dispose();
        }
    }
}