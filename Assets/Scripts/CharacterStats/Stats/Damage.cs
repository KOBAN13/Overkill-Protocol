using System;
using Character.Interface;
using CharacterStats.Health.Interface;

namespace CharacterStats.Health
{
    public class Damage : IDamageable
    {
        private readonly IHealth _health;

        public Damage(IHealth health)
        {
            _health = health;
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0) 
                throw new ArgumentOutOfRangeException();
            
            _health.HealthStats.SetDamage(amount);
        }
    }
}