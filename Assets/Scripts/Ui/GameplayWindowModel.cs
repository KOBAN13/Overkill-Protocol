using System;
using CharactersStats.Interface;
using CharactersStats.Stats;
using Localization.Configs;
using R3;
using Zenject;

namespace Ui
{
    public class GameplayWindowModel : IInitializable, IDisposable
    {
        private readonly StatsCollection _statsCollection;
        private readonly IUpgradeWindowViewTexts _texts;
        private readonly CompositeDisposable _disposables = new();
        
        private readonly ReactiveProperty<string> _healthTitle = new(string.Empty);
        private readonly ReactiveProperty<string> _upgradeWindowText = new(string.Empty);
        private readonly ReactiveProperty<float> _currentHealth = new();

        public ReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;
        public ReadOnlyReactiveProperty<string> HealthTitle => _healthTitle;
        public ReadOnlyReactiveProperty<string> UpgradeWindowText => _upgradeWindowText;

        public GameplayWindowModel(StatsCollection statsCollection, IUpgradeWindowViewTexts texts)
        {
            _statsCollection = statsCollection;
            _texts = texts;
        }

        public void Initialize()
        {
            var healthStat = _statsCollection.GetStat<IHealthStat>(ECharacterStat.Health);
            
            _currentHealth.Value = healthStat.CurrentHealth;

            healthStat.OnCurrentValueChanged
                .Subscribe(value => _currentHealth.Value = value)
                .AddTo(_disposables);

            _healthTitle.Value = _texts.HealthLabel;
            _upgradeWindowText.Value = _texts.OpenUpdateWindowButton;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
