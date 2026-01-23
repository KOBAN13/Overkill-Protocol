using System;
using CharactersStats.Interface;
using R3;
using UnityEngine;

namespace CharactersStats.Stats
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
        
        public void UpgradeStat(int points)
        {
            var baseSpeed = _config.BaseSpeed;
            
            var perPoint = _config.BuffSpeedInPercentage / 100f;
            
            var multiplier = 1f + points * perPoint;
            var maxMultiplier = _config.MaxBuffSpeedInPercentage / 100f;
            multiplier = Mathf.Min(multiplier, maxMultiplier);
    
            var updatedSpeed = baseSpeed * multiplier;
            
            updatedSpeed = Mathf.Clamp(updatedSpeed, _baseValue, _maxValue);

            _currentSpeed.Value = updatedSpeed;

            Debug.Log($"Current speed: {updatedSpeed} (points={points}, x{multiplier:0.###})");
        }
        
        public void Dispose()
        {
            
        }
    }
}