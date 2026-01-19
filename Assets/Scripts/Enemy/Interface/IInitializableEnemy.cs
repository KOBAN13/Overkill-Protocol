using Character.Interface;
using CharacterStats.Interface;
using Game.Stats.Interface;

namespace Enemy.Interface
{
    public interface IInitializableEnemy
    {
        void InitEnemy(IHealthStats healthStats, IDamageable damageable, IEnemyMove enemyMove, IKill kill);
    }
}