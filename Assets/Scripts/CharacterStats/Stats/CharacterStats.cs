using System;
using System.Collections.Generic;
using CharacterStats.Interface;
using Game.Stats.Interface;
using Helper;
using R3;

namespace CharacterStats.Stats
{
    public class CharacterStats : IDisposable
    {
        private readonly Dictionary<ECharacterStat, ICharacterStat> _characterStats = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private static readonly Subject<Unit> OnAnyCharacterStatChange = new();
        private IStatConfigProvider _configProvider;

        private static readonly Action<float> OnStatValueChanged = _ => OnAnyCharacterStatChange.OnNext(Unit.Default);
        public Observable<Unit> OnAnyStatChange => OnAnyCharacterStatChange;
        
        public TStat GetStat<TStat>(ECharacterStat characterStatType) where TStat : class
        {
            if (_characterStats.TryGetValue(characterStatType, out var stat))
            {
                return stat as TStat;
            }
            
            return null;
        }

        public bool TryGetStat<TStat>(ECharacterStat characterStatType, out TStat stat) where TStat : class, ICharacterStat
        {
            stat = GetStat<TStat>(characterStatType);
            return stat != null;
        }

        public void SetConfigProvider(IStatConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void AddStat(ICharacterStatConfig stat)
        {
            var config = _configProvider.GetConfig(stat.StatType);
                
            AddStat(stat, config);
        }

        public void AddStat(ICharacterStatConfig stat, IStatConfig config)
        {
            Preconditions.CheckNotNull(stat, nameof(stat));
            Preconditions.CheckNotNull(config, nameof(config));

            if (_characterStats.TryGetValue(stat.StatType, out var existing))
            {
                if (existing is ICharacterStatConfig existingConfig)
                {
                    existingConfig.Initialize(config);
                    return;
                }

                throw new ArgumentException($"Stat {stat.StatType} already exists with incompatible type");
            }

            stat.Initialize(config);
            _characterStats.Add(stat.StatType, stat);

            stat.OnCurrentValueChanged
                .Subscribe(OnStatValueChanged)
                .AddTo(_compositeDisposable);
        }

        public TStat GetOrAddStat<TStat>(ECharacterStat statType, Func<TStat> create)
            where TStat : class, ICharacterStatConfig
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
            OnAnyCharacterStatChange.Dispose();
            foreach (var stat in _characterStats.Values)
            {
                stat.Dispose();
            }
            _characterStats.Clear();
            _compositeDisposable.Dispose();
        }
    }
}
