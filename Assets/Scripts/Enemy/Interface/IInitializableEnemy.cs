using Character.Interface;
using Enemy;
using Enemy.Interface;
using Game.Stats.Interface;

namespace CharacterStats.Health.Interface
{
    public interface IInitializableEnemy
    {
        void InitEnemy(IHealthStats healthStats, IDamageable damageable, IEnemyMove enemyMove, IKill kill);
    }
}