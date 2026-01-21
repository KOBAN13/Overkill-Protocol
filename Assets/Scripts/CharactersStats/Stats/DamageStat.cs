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

        public void UpgradeStat(int points)
        {
            var baseDamage = _config.BaseDamage;
            
            var perPoint = _config.BuffDamageInPercentage / 100f;
            
            var multiplier = 1f + points * perPoint;
            var maxMultiplier = _config.MaxBuffDamageInPercentage / 100f;
            multiplier = Mathf.Min(multiplier, maxMultiplier);
    
            var updatedDamage = baseDamage * multiplier;
            
            updatedDamage = Mathf.Clamp(updatedDamage, _baseValue, _maxValue);

            _currentDamage.Value = updatedDamage;

            Debug.Log($"Current damage: {updatedDamage} (points={points}, x{multiplier:0.###})");
        }

    }
}
