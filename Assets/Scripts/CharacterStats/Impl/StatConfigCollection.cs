using System;
using System.Collections.Generic;
using CharacterStats.Interface;
using CharacterStats.Stats;
using UnityEngine;

namespace CharacterStats.Impl
{
    [CreateAssetMenu(fileName = nameof(StatConfigCollection), menuName = "Stats" + "/" + nameof(StatConfigCollection))]
    public class StatConfigCollection : ScriptableObject, IStatConfigProvider
    {
        [SerializeField] private List<StatConfig> _configs = new();
        private Dictionary<ECharacterStat, StatConfig> _lookup;

        public TConfig GetConfig<TConfig>(ECharacterStat statType) where TConfig : class, IStatConfig
        {
            EnsureLookup();

            if (!_lookup.TryGetValue(statType, out var config) || config == null)
                throw new ArgumentException($"Config for {statType} is missing");
            
            if (config is TConfig typedConfig)
                return typedConfig;

            throw new ArgumentException($"Config for {statType} is not {typeof(TConfig).Name}");
        }

        private void EnsureLookup()
        {
            _lookup = new Dictionary<ECharacterStat, StatConfig>();
            
            foreach (var config in _configs)
            {
                _lookup[config.StatType] = config;
            }
        }
    }
}
