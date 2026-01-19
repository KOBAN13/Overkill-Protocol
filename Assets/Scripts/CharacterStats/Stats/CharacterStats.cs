using System;
using System.Collections.Generic;
using CharacterStats.Interface;
using Game.Stats.Interface;
using R3;

namespace CharacterStats.Stats
{
    public class CharacterStats : IDisposable
    {
        private readonly Dictionary<ECharacterStat, ICharacterStat> _characterStats = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private static readonly Subject<Unit> OnAnyCharacterStatChange = new();

        private static readonly Action<float> OnStatValueChanged = _ => OnAnyCharacterStatChange.OnNext(Unit.Default);
        
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

        public void AddStat<TConfig>(ICharacterStatConfig<TConfig> stat, TConfig config)
            where TConfig : IStatConfig
        {
            if (_characterStats.ContainsKey(stat.StatType))
            {
                throw new ArgumentException($"Stat {stat.StatType} already exists");
            }

            stat.Initialize(config);
            _characterStats.Add(stat.StatType, stat);

            stat.OnCurrentValueChanged
                .Subscribe(OnStatValueChanged)
                .AddTo(_compositeDisposable);
        }

        public Observable<Unit> OnAnyStatChange => OnAnyCharacterStatChange;
        
        public Observable<TStat> OnStatChange<TStat>(ECharacterStat statType) where TStat : class, ICharacterStat
        {
            if (!_characterStats.TryGetValue(statType, out var stat) || stat is not TStat typedStat)
            {
                return Observable.Empty<TStat>();
            }

            return typedStat.OnCurrentValueChanged.Select(_ => typedStat);
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