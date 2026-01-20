using System;
using System.Collections.Generic;
using Character;
using CharactersStats.Interface;
using CharacterStats.Stats;
using CharacterStats.Interface;
using CharacterStats.Stats;
using Enemy.Config;
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
        private readonly IStatConfigProvider _statConfigProvider;
        private readonly PlayerComponents _playerComponents;
        private readonly TickableManager _tickableManager;
        private readonly Dictionary<HumanoidEnemy, EnemyRuntime> _runtime = new();

        public EnemyFactory(
            IEnemyPool enemyPool,
            EnemyParameters enemyParameters,
            PlayerComponents playerComponents,
            TickableManager tickableManager, 
            IStatConfigProvider statConfigProvider)
        {
            _enemyPool = enemyPool;
            _enemyParameters = enemyParameters;
            _playerComponents = playerComponents;
            _tickableManager = tickableManager;
            _statConfigProvider = statConfigProvider;
        }

        public HumanoidEnemy Spawn(Vector3 position, Quaternion rotation)
        {
            var enemy = _enemyPool.Get(position, rotation);

            var runtime = GetOrCreateRuntime(enemy);
            runtime.Stats.SetConfigProvider(_statConfigProvider);
            runtime.Stats.AddStat(runtime.Health);
            runtime.Health.SetDie(runtime.Die);

            _tickableManager.Add(runtime.EnemyMove);
            enemy.InitEnemy(runtime.Damage, runtime.EnemyMove);

            return enemy;
        }

        public void Despawn(HumanoidEnemy enemy)
        {
            if (_runtime.TryGetValue(enemy, out var runtime))
            {
                _tickableManager.Remove(runtime.EnemyMove);
            }

            _enemyPool.Release(enemy);
        }

        public void Dispose()
        {
            foreach (var entry in _runtime)
            {
                _tickableManager.Remove(entry.Value.EnemyMove);
                entry.Value.Stats.Dispose();
                _enemyPool.Release(entry.Key);
            }

            _runtime.Clear();
        }

        private EnemyRuntime GetOrCreateRuntime(HumanoidEnemy enemy)
        {
            if (_runtime.TryGetValue(enemy, out var runtime))
            {
                return runtime;
            }

            var enemyMove = new EnemyWalk(_playerComponents, _enemyParameters.Speed);
            var die = new PooledEnemyDie(() => Despawn(enemy));
            var health = new HealthCharacter();
            var damage = new Damage(health);
            var stats = new StatsCollection();

            runtime = new EnemyRuntime(enemyMove, stats, health, damage, die);
            
            _runtime.Add(enemy, runtime);

            return runtime;
        }

        private sealed class EnemyRuntime
        {
            public EnemyRuntime(
                EnemyWalk enemyMove,
                StatsCollection stats,
                HealthCharacter health,
                Damage damage,
                PooledEnemyDie die)
            {
                EnemyMove = enemyMove;
                Stats = stats;
                Health = health;
                Damage = damage;
                Die = die;
            }

            public EnemyWalk EnemyMove { get; }
            public StatsCollection Stats { get; }
            public HealthCharacter Health { get; }
            public Damage Damage { get; }
            public PooledEnemyDie Die { get; }
        }
    }
}
