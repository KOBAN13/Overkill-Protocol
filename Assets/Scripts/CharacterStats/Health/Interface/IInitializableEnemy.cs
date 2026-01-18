using Enemy.Interface;
using Enemy.Walk;
using IKill = Enemy.EnemyKill.IKill;

namespace Enemy
{
    public interface IInitializableEnemy
    {
        void InitEnemy(IHealthStats healthStats, IDamagable damagable, IEnemyMove enemyMove, IKill kill, float point);
    }
}