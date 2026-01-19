using Enemy;
using Enemy.Interface;

namespace CharacterStats.Health.Interface
{
    public interface IInitializableEnemy
    {
        void InitEnemy(IHealthStats healthStats, IDamagable damagable, IEnemyMove enemyMove, IKill kill);
    }
}