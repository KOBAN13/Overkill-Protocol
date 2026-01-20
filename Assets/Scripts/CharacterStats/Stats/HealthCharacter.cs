using System;
using System.Threading;
using CharacterStats.Die;
using CharacterStats.Interface;
using Helper;
using R3;
using UnityEngine;

namespace CharacterStats.Stats
{
    public class HealthCharacter : IHealthStat, IDisposable
    {
        public ECharacterStat StatType => ECharacterStat.Health;
        public Observable<float> OnCurrentValueChanged => _currentHealth;
        public ReadOnlyReactiveProperty<float> CurrentHealthPercentage => _amountHealthPercentage;
        public float CurrentHealth => _currentHealth.Value;

        private CancellationTokenSource _cancellationTokenSource;
        private IHealthConfig _config;
        private IDie _die;
        private bool _isDead;
        private float _baseValue;
        private float _maxValue;
        
        private readonly ReactiveProperty<float> _currentHealth = new();
        private readonly ReactiveProperty<float> _amountHealthPercentage = new();
        
        public void Initialize(IHealthConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _isDead = false;
            _baseValue = _currentHealth.Value = _config.BaseValue;
            _maxValue = _config.BaseValue * (1 + _config.MaxBuffHealthInPercentage / 100);
            _amountHealthPercentage.Value = 1f;
        }
        
        public void ResetHealthStat()
        {
            _isDead = false;
            _baseValue = _currentHealth.Value = _config.BaseValue;
            _amountHealthPercentage.Value = 1f;
        }
        
        public void SetDamage(float value)
        {
            Preconditions.CheckValidateData(value);

            if (_isDead)
                return;

            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value - value, 0f, _maxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_amountHealthPercentage.Value - value / _baseValue, 0f, 1f);
            
            if (_currentHealth.Value != 0f) 
                return;
            
            TryDie();
        }
        
        public void UpgradeStat()
        {
            var buffSpeed = _config.BaseValue * (1 + _config.BuffHealthInPercentage / 100);
            
            var updateHealth = Mathf.Clamp(_baseValue + buffSpeed, _baseValue, _maxValue);
            
            _currentHealth.Value = updateHealth;

            _amountHealthPercentage.Value = Mathf.Clamp(_amountHealthPercentage.Value / updateHealth, 0f, 1f);
        }

        public void SetDie(IDie die) 
        {
            _die = die;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _currentHealth?.Dispose();
        }

        private void TryDie()
        {
            if (_isDead || _currentHealth.Value > 0f)
                return;

            _isDead = true;
            _die?.Died();
        }
    }
}
