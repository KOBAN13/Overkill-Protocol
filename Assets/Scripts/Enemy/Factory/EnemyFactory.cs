using System;
using System.Collections.Generic;
using Character;
using CharacterStats.Die;
using CharacterStats.Stats;
using Enemy.Config;
using Enemy.EnemyKill;
using Enemy.Pooling;
using Enemy.Walk;
using UnityEngine;
using Zenject;

namespace Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IEnemyPool _enemyPool;
        private readonly EnemyParameters _enemyParameters;
        private readonly PlayerComponents _playerComponents;
        private readonly TickableManager _tickableManager;
        private readonly Dictionary<HumanoidEnemy, EnemyRuntime> _runtime = new();

        public EnemyFactory(
            IEnemyPool enemyPool,
            EnemyParameters enemyParameters,
            PlayerComponents playerComponents,
            TickableManager tickableManager)
        {
            _enemyPool = enemyPool;
            _enemyParameters = enemyParameters;
            _playerComponents = playerComponents;
            _tickableManager = tickableManager;
        }

        public HumanoidEnemy Spawn(Vector3 position, Quaternion rotation)
        {
            var enemy = _enemyPool.Get(position, rotation);

            var enemyMove = new EnemyWalk(_playerComponents, _enemyParameters.Speed);
            _tickableManager.Add(enemyMove);

            var kill = new HumanoidEnemyKill(new Die(_playerComponents));
            var die = new PooledEnemyDie(() => Despawn(enemy));
            var damage = new Damage(enemy);

            enemy.InitEnemy(health, damage, enemyMove, kill);
            
            _runtime[enemy] = new EnemyRuntime(enemyMove, kill);

            return enemy;
        }

        public void Despawn(HumanoidEnemy enemy)
        {
            if (_runtime.TryGetValue(enemy, out var runtime))
            {
                _tickableManager.Remove(runtime.EnemyMove);
                runtime.Kill.Dispose();
                _runtime.Remove(enemy);
            }

            _enemyPool.Release(enemy);
        }

        public void Dispose()
        {
            foreach (var entry in _runtime)
            {
                _tickableManager.Remove(entry.Value.EnemyMove);
                entry.Value.Kill.Dispose();
                _enemyPool.Release(entry.Key);
            }

            _runtime.Clear();
        }

        private readonly struct EnemyRuntime
        {
            public EnemyRuntime(EnemyWalk enemyMove, HumanoidEnemyKill kill)
            {
                EnemyMove = enemyMove;
                Kill = kill;
            }

            public EnemyWalk EnemyMove { get; }
            public HumanoidEnemyKill Kill { get; }
        }
    }
}
