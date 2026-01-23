using System;
using Character.Interface;
using CharactersStats.Interface;

namespace CharactersStats.Stats
{
    public class Damage : IDamageable
    {
        private readonly IHealthStat _health;

        public Damage(IHealthStat health)
        {
            _health = health;
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0) 
                throw new ArgumentOutOfRangeException();
            
            _health.SetDamage(amount);
        }
    }
}