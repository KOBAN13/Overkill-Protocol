using System;
using System.Threading;
using CharacterStats.Health.Die;
using CharacterStats.Stats.Impl;
using Cysharp.Threading.Tasks;
using Game.Stats.Interface;
using Helper;
using R3;
using UnityEngine;

namespace CharacterStats.Stats
{
    public class HealthCharacter : IHealthStats, ICharacterStatConfig<HealthConfig>, IDisposable
    {
        public ECharacterStat StatType => ECharacterStat.Health;
        public float MaxValue { get; private set; }
        public Observable<float> OnCurrentValueChanged => _currentHealth;
        public ReadOnlyReactiveProperty<float> CurrentHealthPercentage => _amountHealthPercentage;
        public float CurrentHealth => _currentHealth.Value;

        private CancellationTokenSource _cancellationTokenSource;
        private HealthConfig _config;
        private IDie _die;
        private bool _isDead;
        
        private readonly ReactiveProperty<float> _currentHealth = new();
        private readonly ReactiveProperty<float> _amountHealthPercentage = new();
        
        public void Initialize(HealthConfig config)
        {
            _config = config;
            MaxValue = _currentHealth.Value = _config.MaxValue;
            _amountHealthPercentage.Value = 1f;
        }
        
        public void ResetHealthStat()
        {
            _isDead = false;
            MaxValue = _currentHealth.Value = _config.MaxValue;
            _amountHealthPercentage.Value = 1f;
        }
        
        public void SetDamage(float value)
        {
            Preconditions.CheckValidateData(value);

            if (_isDead)
                return;

            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value - value, 0f, MaxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_amountHealthPercentage.Value - value / MaxValue, 0f, 1f);
            
            if (_currentHealth.Value != 0f) 
                return;
            
            TryDie();
        }

        public async UniTaskVoid AddHealth(float value)
        {
            Preconditions.CheckValidateData(value);

            if (_isDead)
                return;

            _currentHealth.Value = Mathf.Clamp(value + _currentHealth.Value, 0f, MaxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_currentHealth.Value / MaxValue, 0f, 1f);
            
            await UniTask.Yield();
        }

        public async UniTaskVoid SetHealth(float value)
        {
            Preconditions.CheckValidateData(value);

            _currentHealth.Value = Mathf.Clamp(value, 0f, MaxValue);

            _amountHealthPercentage.Value = Mathf.Clamp(_currentHealth.Value / MaxValue, 0f, 1f);

            TryDie();
            
            await UniTask.Yield();
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
