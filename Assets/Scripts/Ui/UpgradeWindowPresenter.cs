using CharactersStats.Stats;
using R3;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class UpgradeWindowPresenter : PresenterBase, IInitializable
    {
        private const int BaseUpgradeLevel = 1;

        private readonly UpgradeWindowModel _model;
        private readonly UpgradeWindowView _view;
        private readonly GameplayWindowView _gameplayWindow;

        private int _countHealthPoint;
        private int _countDamagePoint;
        private int _countSpeedPoint;
        
        private int _totalUpgradePoints;
        
        private int _appliedHealthPoint;
        private int _appliedDamagePoint;
        private int _appliedSpeedPoint;
        
        public UpgradeWindowPresenter(
            UpgradeWindowModel model, 
            UpgradeWindowView view, 
            GameplayWindowView gameplayWindow
        )
        {
            _model = model;
            _view = view;
            _gameplayWindow = gameplayWindow;
        }

        public void Initialize()
        {
            BindLocalizationText();
            InitializeCounters();
            
            Bind(_model.AddUpgradePoints, points =>
            {
                _totalUpgradePoints = points;
                UpdateRemainingPoints();
            });

            BindButtons();
            UpdateRemainingPoints();
        }

        private void BindButtons()
        {
            BindClick(_view.CloseWindow.OnClickAsObservable(), () =>
            {
                _view.CanvasGroup.alpha = 0;
                _gameplayWindow.CanvasGroup.alpha = 1;
            });
            
            BindClick(_view.Apply.OnClickAsObservable(), () =>
            {
                var damageDelta = Mathf.Max(0, _countDamagePoint - _appliedDamagePoint);
                var healthDelta = Mathf.Max(0, _countHealthPoint - _appliedHealthPoint);
                var speedDelta = Mathf.Max(0, _countSpeedPoint - _appliedSpeedPoint);

                if (damageDelta > 0)
                    _model.SpendUpgradePoints(ECharacterStat.Damage, damageDelta);

                if (healthDelta > 0)
                    _model.SpendUpgradePoints(ECharacterStat.Health, healthDelta);

                if (speedDelta > 0)
                    _model.SpendUpgradePoints(ECharacterStat.Speed, speedDelta);

                _appliedDamagePoint = _countDamagePoint;
                _appliedHealthPoint = _countHealthPoint;
                _appliedSpeedPoint = _countSpeedPoint;

                _view.CanvasGroup.alpha = 0;
                _gameplayWindow.CanvasGroup.alpha = 1;
            });
            
            BindClick(_view.UpgradeDamage.OnClickAsObservable(), () =>
            {
                if (GetRemainingPoints() == 0)
                    return;

                _countDamagePoint++;
                _view.SetDamageUpdatePoint(_countDamagePoint);
                UpdateRemainingPoints();
            });
            
            BindClick(_view.UpgradeHealth.OnClickAsObservable(), () =>
            {
                if (GetRemainingPoints() == 0)
                    return;

                _countHealthPoint++;
                _view.SetHealthUpdatePoint(_countHealthPoint);
                UpdateRemainingPoints();
            });
            
            BindClick(_view.UpgradeSpeed.OnClickAsObservable(), () =>
            {
                if (GetRemainingPoints() == 0)
                    return;

                _countSpeedPoint++;
                _view.SetSpeedUpdatePoint(_countSpeedPoint);
                UpdateRemainingPoints();
            });
        }

        private void BindLocalizationText()
        {
            Bind(_model.Title, _view.UpdateTitleText);
            Bind(_model.PointsLabel, _view.UpdatePointsLabel);
            Bind(_model.SpeedLabel, _view.UpdateSpeedLabel);
            Bind(_model.DamageLabel, _view.UpdateDamageLabel);
            Bind(_model.HealthLabel, _view.UpdateHealthLabel);
            Bind(_model.ApplyButtonText, _view.UpdateApplyButtonText);
        }

        private void InitializeCounters()
        {
            _countHealthPoint = BaseUpgradeLevel;
            _countDamagePoint = BaseUpgradeLevel;
            _countSpeedPoint = BaseUpgradeLevel;
            _appliedHealthPoint = BaseUpgradeLevel;
            _appliedDamagePoint = BaseUpgradeLevel;
            _appliedSpeedPoint = BaseUpgradeLevel;

            _view.SetHealthUpdatePoint(_countHealthPoint);
            _view.SetDamageUpdatePoint(_countDamagePoint);
            _view.SetSpeedUpdatePoint(_countSpeedPoint);
        }

        private int GetRemainingPoints()
        {
            var reservedPoints = Mathf.Max(0, _countDamagePoint - _appliedDamagePoint)
                                 + Mathf.Max(0, _countHealthPoint - _appliedHealthPoint)
                                 + Mathf.Max(0, _countSpeedPoint - _appliedSpeedPoint);

            return Mathf.Max(0, _totalUpgradePoints - reservedPoints);
        }

        private void UpdateRemainingPoints()
        {
            var remainingPoints = GetRemainingPoints();
            _view.SetCountUpgradePoint(remainingPoints);
            var canUpgrade = remainingPoints > 0;
            _view.UpgradeDamage.interactable = canUpgrade;
            _view.UpgradeHealth.interactable = canUpgrade;
            _view.UpgradeSpeed.interactable = canUpgrade;
        }
        
    }
}
