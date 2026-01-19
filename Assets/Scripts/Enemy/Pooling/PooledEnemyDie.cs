using System;
using CharacterStats.Die;

namespace Enemy.Pooling
{
    public class PooledEnemyDie : IDie
    {
        private readonly Action _onDied;

        public PooledEnemyDie(Action onDied)
        {
            _onDied = onDied;
        }

        public void Died()
        {
            _onDied?.Invoke();
        }
    }
}
