using CharactersStats.Stats;
using Helper;
using Localization.Configs;
using R3;
using Ui.Data;
using Zenject;

namespace Ui
{
    public class UpgradeWindowModel : IInitializable
    {
        private readonly IUpgradeWindowViewTexts _texts;

        private readonly ReactiveProperty<string> _title = new(string.Empty);
        private readonly ReactiveProperty<string> _pointsLabel = new(string.Empty);
        private readonly ReactiveProperty<string> _healthLabel = new(string.Empty);
        private readonly ReactiveProperty<string> _speedLabel = new(string.Empty);
        private readonly ReactiveProperty<string> _damageLabel = new(string.Empty);
        private readonly ReactiveProperty<string> _applyButtonText = new(string.Empty);

        private readonly ReactiveProperty<int> _addUpgradePoints = new();
        private readonly ReactiveProperty<UpgradePointData> _spentUpgradePoints = new();
        
        public ReadOnlyReactiveProperty<string> Title => _title;
        public ReadOnlyReactiveProperty<string> PointsLabel => _pointsLabel;
        public ReadOnlyReactiveProperty<string> HealthLabel => _healthLabel;
        public ReadOnlyReactiveProperty<string> SpeedLabel => _speedLabel;
        public ReadOnlyReactiveProperty<string> DamageLabel => _damageLabel;
        public ReadOnlyReactiveProperty<string> ApplyButtonText => _applyButtonText;
        
        public ReadOnlyReactiveProperty<int> AddUpgradePoints => _addUpgradePoints;
        public ReadOnlyReactiveProperty<UpgradePointData> SpentUpgradePoints => _spentUpgradePoints;
        
        public UpgradeWindowModel(UpgradeWindowViewTexts texts)
        {
            _texts = texts;
        }

        public void AddUpdatePoint(int points)
        {
            Preconditions.CheckValidateData(points);
            _addUpgradePoints.Value = points;
        }

        public void SpendUpgradePoints(ECharacterStat stat, int upgradePoints)
        {
            Preconditions.CheckValidateData(upgradePoints);
            _spentUpgradePoints.Value = new UpgradePointData(upgradePoints, stat);
        }

        public void Initialize()
        {
            _title.Value = _texts.Title;
            _pointsLabel.Value = _texts.PointsLabel;
            _healthLabel.Value = _texts.HealthLabel;
            _speedLabel.Value = _texts.SpeedLabel;
            _damageLabel.Value = _texts.DamageLabel;
            _applyButtonText.Value = _texts.ApplyButton;
        }
    }
}