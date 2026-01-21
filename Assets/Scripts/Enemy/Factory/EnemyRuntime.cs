using CharactersStats.Stats;
using CharacterStats.Stats;
using CharacterStats.Stats;
using Enemy.Pooling;
using Enemy.Walk;

namespace Enemy.Factory
{
    public sealed class EnemyRuntime
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