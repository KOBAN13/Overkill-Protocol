using System;
using Enemy.Config;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Enemy.Pooling
{
    public class EnemyPool : IEnemyPool, IDisposable
    {
        private const int DefaultCapacity = 8;
        private const int MaxCapacity = 64;

        private readonly EnemyParameters _enemyParameters;
        private readonly ObjectPool<HumanoidEnemy> _pool;
        private readonly DiContainer _diContainer;

        public EnemyPool(EnemyParameters enemyParameters, DiContainer diContainer)
        {
            _enemyParameters = enemyParameters;
            _diContainer = diContainer;

            _pool = new ObjectPool<HumanoidEnemy>(CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, true,
                DefaultCapacity, MaxCapacity);
        }

        public HumanoidEnemy Get(Vector3 position, Quaternion rotation)
        {
            var enemy = _pool.Get();
            enemy.transform.SetPositionAndRotation(position, rotation);
            return enemy;
        }

        public void Release(HumanoidEnemy enemy)
        {
            _pool.Release(enemy);
        }

        public void Dispose()
        {
            _pool.Clear();
        }

        private HumanoidEnemy CreateEnemy()
        {
            var prefab = _enemyParameters.EnemyPrefab;
            var enemy = _diContainer.InstantiatePrefabForComponent<HumanoidEnemy>(prefab);
            
            Debug.Log($"{prefab.name} created");
            
            return enemy;
        }

        private static void OnGetEnemy(HumanoidEnemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private static void OnReleaseEnemy(HumanoidEnemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private static void OnDestroyEnemy(HumanoidEnemy enemy)
        {
            if (enemy != null)
            {
                UnityEngine.Object.Destroy(enemy.gameObject);
            }
        }
    }
}
