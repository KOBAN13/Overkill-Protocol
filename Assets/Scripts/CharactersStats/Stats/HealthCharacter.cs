using System;
using System.Threading;
using CharactersStats.Die;
using CharactersStats.Interface;
using Helper;
using R3;
using UnityEngine;

namespace CharactersStats.Stats
{
    public class HealthCharacter : IHealthStat, IDisposable
    {
        public ECharacterStat StatType => ECharacterStat.Health;
        public ReadOnlyReactiveProperty<float> OnCurrentValueChanged => _currentHealth;
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
        
        public void UpgradeStat(int points)
        {
            var perPoint = _config.BuffHealthInPercentage / 100f; 
            var multiplier = 1f + points * perPoint;
            
            var maxMultiplier = _config.MaxBuffHealthInPercentage / 100f;
            multiplier = Mathf.Min(multiplier, maxMultiplier);
            
            var updatedHealth = _baseValue * multiplier;
            
            updatedHealth = Mathf.Clamp(updatedHealth, _baseValue, _baseValue * maxMultiplier);

            _currentHealth.Value = updatedHealth;

            Debug.Log($"Current health: {updatedHealth} (points={points}, x{multiplier:0.###})");
            
            _amountHealthPercentage.Value = Mathf.Clamp(_amountHealthPercentage.Value / updatedHealth, 0f, 1f);
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
