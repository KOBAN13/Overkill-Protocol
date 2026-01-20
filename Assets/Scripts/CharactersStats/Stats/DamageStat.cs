using System;
using CharacterStats.Interface;
using R3;
using UnityEngine;

namespace CharacterStats.Stats
{
    public class DamageStat : IDamageStat
    {
        public ECharacterStat StatType => ECharacterStat.Damage;

        public ReadOnlyReactiveProperty<float> CurrentDamage => _currentDamage;

        private float _baseValue;
        private float _maxValue;
        private IDamageConfig _config;
        private readonly ReactiveProperty<float> _currentDamage = new();

        public void Initialize(IDamageConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _baseValue = _currentDamage.Value = _config.BaseDamage;
            _maxValue = _config.BaseDamage * (1 + _config.MaxBuffDamageInPercentage / 100);
        }

        public void Dispose()
        {
        }

        public void UpgradeStat()
        {
            var buffDamage = _config.BaseDamage * (1 + _config.BuffDamageInPercentage / 100);

            var updateValue = Mathf.Clamp(_baseValue + buffDamage, _baseValue, _maxValue);

            _currentDamage.Value = updateValue;
        }
    }
}
