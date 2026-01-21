using System;
using System.Collections.Generic;
using System.Linq;
using CharactersStats.Interface;
using CharacterStats.Impl;
using CharacterStats.Interface;
using CharacterStats.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils.Enums;

namespace CharactersStats.Impl
{
    [CreateAssetMenu(fileName = nameof(StatConfigCollection), menuName = "Stats" + "/" + nameof(StatConfigCollection))]
    public class StatConfigCollection : SerializedScriptableObject, IStatConfigProvider
    {
        [OdinSerialize] private IReadOnlyDictionary<EStatsOwner, List<StatConfig>> _configs;

        public TConfig GetConfig<TConfig>(EStatsOwner statsOwner, ECharacterStat statType) where TConfig : class, IStatConfig
        {
            if (!_configs.TryGetValue(statsOwner, out var configs))
                throw new ArgumentException($"Configs for {statsOwner} is missing");
            
            var config = configs.FirstOrDefault(type => type.StatType == statType);
            
            if (config is TConfig typedConfig)
                return typedConfig;

            throw new ArgumentException($"Config for {statType} is not {typeof(TConfig).Name}");
        }
    }
}
