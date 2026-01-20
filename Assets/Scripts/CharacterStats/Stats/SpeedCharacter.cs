using System;
using CharacterStats.Interface;
using R3;
using UnityEngine;

namespace CharacterStats.Stats
{
    public class SpeedCharacter : ISpeedStat
    {
        public ECharacterStat StatType => ECharacterStat.Speed;

        public ReadOnlyReactiveProperty<float> CurrentSpeed => _currentSpeed;
        
        private float _baseValue;
        private float _maxValue;
        private ISpeedConfig _config;
        private readonly ReactiveProperty<float> _currentSpeed = new();
        
        public void Initialize(ISpeedConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _baseValue = _currentSpeed.Value = _config.BaseSpeed;
            _maxValue = _config.BaseSpeed * (1 + _config.MaxBuffSpeedInPercentage / 100);
        }
        
        public void UpdateSpeed()
        {
            var buffSpeed = _config.BaseSpeed * (1 + _config.BuffSpeedInPercentage / 100);
            
            var updateValue = Mathf.Clamp(_baseValue + buffSpeed, _baseValue, _maxValue);
            
            _currentSpeed.Value = updateValue;
        }
        
        public void Dispose()
        {
            
        }
    }
}