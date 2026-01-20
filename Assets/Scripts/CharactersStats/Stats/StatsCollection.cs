using System;
using System.Collections.Generic;
using CharacterStats.Interface;
using Helper;

namespace CharacterStats.Stats
{
    public class StatsCollection : IDisposable
    {
        private readonly Dictionary<ECharacterStat, ICharacterStat> _characterStats = new();
        private IStatConfigProvider _configProvider;
        
        public TStat GetStat<TStat>(ECharacterStat characterStatType) where TStat : class, ICharacterStat
        {
            return TryGetStat(characterStatType, out TStat stat) ? stat : null;
        }

        public bool TryGetStat<TStat>(ECharacterStat characterStatType, out TStat stat)
            where TStat : class, ICharacterStat
        {
            if (_characterStats.TryGetValue(characterStatType, out var existing) && existing is TStat typed)
            {
                stat = typed;
                return true;
            }

            stat = null;
            return false;
        }

        public void SetConfigProvider(IStatConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void AddStat<TConfig>(ICharacterStatConfig<TConfig> stat)
            where TConfig : class, IStatConfig
        {
            var config = _configProvider.GetConfig<TConfig>(stat.StatType);
                
            AddStat(stat, config);
        }

        public void AddStat<TConfig>(ICharacterStatConfig<TConfig> stat, TConfig config)
            where TConfig : class, IStatConfig
        {
            Preconditions.CheckNotNull(stat, nameof(stat));
            Preconditions.CheckNotNull(config, nameof(config));

            if (_characterStats.TryGetValue(stat.StatType, out var existing))
            {
                if (existing is ICharacterStatConfig<TConfig> existingConfig)
                {
                    existingConfig.Initialize(config);
                    return;
                }

                throw new ArgumentException($"Stat {stat.StatType} already exists with incompatible type");
            }

            stat.Initialize(config);
            _characterStats.Add(stat.StatType, stat);
        }

        public TStat GetOrAddStat<TStat, TConfig>(ECharacterStat statType, Func<TStat> create)
            where TStat : class, ICharacterStatConfig<TConfig>
            where TConfig : class, IStatConfig
        {
            if (_characterStats.TryGetValue(statType, out var existing))
            {
                if (existing is TStat typed)
                {
                    return typed;
                }

                throw new ArgumentException($"Stat {statType} already exists with incompatible type");
            }

            var stat = create();
            if (stat.StatType != statType)
            {
                throw new ArgumentException($"Stat type mismatch. Expected {statType}, got {stat.StatType}");
            }

            AddStat(stat);
            return stat;
        }

        public void Dispose()
        {
            foreach (var stat in _characterStats.Values)
            {
                stat.Dispose();
            }
            
            _characterStats.Clear();
        }
    }
}
