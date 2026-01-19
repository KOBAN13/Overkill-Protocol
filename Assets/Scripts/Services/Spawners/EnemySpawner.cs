using System;
using Enemy.Factory;
using R3;
using Services.Config;
using UnityEngine;
using Zenject;

namespace Services.Spawners
{
    public class EnemySpawner : IInitializable, IDisposable
    {
        private readonly PointsCamera _pointsCamera;
        private readonly EnemySpawnParameters _spawnParameters;
        private readonly IEnemyFactory _enemyFactory;
        private readonly CompositeDisposable _compositeDisposable = new();

        public EnemySpawner(
            PointsCamera pointsCamera,
            EnemySpawnParameters spawnParameters,
            IEnemyFactory enemyFactory)
        {
            _pointsCamera = pointsCamera;
            _spawnParameters = spawnParameters;
            _enemyFactory = enemyFactory;
        }

        public void Initialize()
        {
            var timeToSpawn = _spawnParameters.TimeToSpawnEnemy;
            
            Observable
                .Timer(TimeSpan.FromSeconds(timeToSpawn), TimeSpan.FromSeconds(timeToSpawn))
                .Subscribe(_ => StartSpawn())
                .AddTo(_compositeDisposable);
        }

        private void StartSpawn()
        {
            _pointsCamera.Invisible(out var position);
            _enemyFactory.Spawn(position, Quaternion.identity);
        }

        public void Dispose()
        {
            _compositeDisposable?.Clear();
            _compositeDisposable?.Dispose();
        }
    }
}
