using System;
using CharacterStats.Health.Die;

namespace Enemy.Pooling
{
    public class PooledEnemyDie : IDie<HumanoidEnemy>
    {
        private readonly Action<HumanoidEnemy> _onDied;

        public PooledEnemyDie(Action<HumanoidEnemy> onDied)
        {
            _onDied = onDied;
        }

        public void Died(HumanoidEnemy objectDie)
        {
            _onDied?.Invoke(objectDie);
        }
    }
}
