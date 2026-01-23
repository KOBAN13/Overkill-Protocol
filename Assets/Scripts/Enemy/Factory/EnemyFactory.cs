using System;
using System.Collections.Generic;
using Character;
using CharactersStats.Impl;
using CharactersStats.Interface;
using CharactersStats.Stats;
using Enemy.Config;
using Enemy.Pooling;
using Enemy.Walk;
using UnityEngine;
using Utils.Enums;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IObjectPool<HumanoidEnemy> _enemyPool;
        private readonly EnemyParameters _enemyParameters;
        private readonly IStatConfigProvider _statConfigProvider;
        private readonly PlayerComponents _playerComponents;
        private readonly TickableManager _tickableManager;
        private readonly IUpgradeStats _upgradeStats;
        private readonly Dictionary<HumanoidEnemy, EnemyRuntime> _runtime = new();
        private readonly HashSet<HumanoidEnemy> _activeEnemies = new();

        public EnemyFactory(
            IObjectPool<HumanoidEnemy>  enemyPool,
            EnemyParameters enemyParameters,
            PlayerComponents playerComponents,
            TickableManager tickableManager, 
            IStatConfigProvider statConfigProvider,
            IUpgradeStats upgradeStats)
        {
            _enemyPool = enemyPool;
            _enemyParameters = enemyParameters;
            _playerComponents = playerComponents;
            _tickableManager = tickableManager;
            _statConfigProvider = statConfigProvider;
            _upgradeStats = upgradeStats;

            var enemy = _enemyParameters.EnemyPrefab.GetComponent<HumanoidEnemy>();
            
            _enemyPool.Initialize(enemy);
        }

        public HumanoidEnemy Spawn(Vector3 position, Quaternion rotation)
        {
            var enemy = _enemyPool.Get(position, rotation);

            var runtime = GetOrCreateRuntime(enemy);
            runtime.Stats.SetConfigProvider(_statConfigProvider);
            var baseHealthConfig = _statConfigProvider.GetConfig<HealthConfig>(EStatsOwner.Enemy, ECharacterStat.Health);
            var randomizedBaseHealth = GetRandomizedStartHealth();
            var randomizedHealthConfig = new RandomizedHealthConfig(baseHealthConfig, randomizedBaseHealth);
            runtime.Stats.AddStat(runtime.Health, randomizedHealthConfig);
            runtime.Health.SetDie(runtime.Die);

            _tickableManager.Add(runtime.EnemyMove);
            _activeEnemies.Add(enemy);
            enemy.InitEnemy(runtime.Damage, runtime.EnemyMove);

            return enemy;
        }

        public void Despawn(HumanoidEnemy enemy)
        {
            if (_runtime.TryGetValue(enemy, out var runtime))
            {
                if (!_activeEnemies.Remove(enemy))
                    return;

                _tickableManager.Remove(runtime.EnemyMove);
                _enemyPool.Return(enemy);
                return;
            }
        }

        public void Dispose()
        {
            foreach (var enemy in _activeEnemies)
            {
                if (_runtime.TryGetValue(enemy, out var runtime))
                {
                    _tickableManager.Remove(runtime.EnemyMove);
                    _enemyPool.Return(enemy);
                }
            }

            foreach (var entry in _runtime)
            {
                entry.Value.Stats.Dispose();
            }

            _runtime.Clear();
            _activeEnemies.Clear();
        }

        private EnemyRuntime GetOrCreateRuntime(HumanoidEnemy enemy)
        {
            if (_runtime.TryGetValue(enemy, out var runtime))
            {
                return runtime;
            }

            var enemyMove = new EnemyWalk(_playerComponents, _enemyParameters.Speed);
            var die = new PooledEnemyDie(() =>
            {
                _upgradeStats.AddUpgradePoints();
                Despawn(enemy);
            });
            var health = new HealthCharacter();
            var damage = new Damage(health);
            var stats = new StatsCollection();

            runtime = new EnemyRuntime(enemyMove, stats, health, damage, die);
            
            _runtime.Add(enemy, runtime);

            return runtime;
        }

        private int GetRandomizedStartHealth()
        {
            var min = Mathf.Min(_enemyParameters.MinStartHealth, _enemyParameters.MaxStartHealth);
            var max = Mathf.Max(_enemyParameters.MinStartHealth, _enemyParameters.MaxStartHealth);
            return Random.Range(min, max + 1);
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
