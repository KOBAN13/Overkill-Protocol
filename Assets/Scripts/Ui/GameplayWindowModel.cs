using System;
using CharactersStats.Interface;
using CharactersStats.Stats;
using CharacterStats.Interface;
using CharacterStats.Stats;
using R3;
using Zenject;

namespace Ui
{
    public class GameplayWindowModel : IInitializable, IDisposable
    {
        private readonly StatsCollection _statsCollection;
        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<float> _currentHealth = new();

        public ReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;

        public GameplayWindowModel(StatsCollection statsCollection)
        {
            _statsCollection = statsCollection;
        }

        public void Initialize()
        {
            var healthStat = _statsCollection.GetStat<IHealthStat>(ECharacterStat.Health);
            if (healthStat == null)
                return;

            _currentHealth.Value = healthStat.CurrentHealth;

            healthStat.CurrentHealthPercentage
                .Subscribe(value => _currentHealth.Value = value)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
