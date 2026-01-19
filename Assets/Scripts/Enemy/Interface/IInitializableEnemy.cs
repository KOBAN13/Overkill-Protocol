using Character.Interface;

namespace Enemy.Interface
{
    public interface IInitializableEnemy
    {
        void InitEnemy(IDamageable damageable, IEnemyMove enemyMove);
    }
}