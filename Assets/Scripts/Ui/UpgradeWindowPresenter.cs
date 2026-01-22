using System;
using CharacterStats.Stats;
using R3;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class UpgradeWindowPresenter : IInitializable, IDisposable
    {
        private readonly UpgradeWindowModel _model;
        private readonly UpgradeWindowView _view;

        private readonly CompositeDisposable _disposables = new();

        private int _countHealthPoint = 1;
        private int _countDamagePoint = 1;
        private int _countSpeedPoint = 1;
        private int _totalUpgradePoints;
        
        public UpgradeWindowPresenter(UpgradeWindowModel model, UpgradeWindowView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            SubscribeLocalizationText();
            
            _model.AddUpgradePoints.
                Subscribe(points =>
                {
                    _totalUpgradePoints = points;
                    UpdateRemainingPoints();
                })
                .AddTo(_disposables);

            SubscribeButtons();
            UpdateRemainingPoints();
        }

        private void SubscribeButtons()
        {
            _view.CloseWindow
                .OnClickAsObservable()
                .Subscribe(_ => _view.gameObject.SetActive(false))
                .AddTo(_disposables);
            
            _view.Apply.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _model.SpendUpgradePoints(ECharacterStat.Damage, _countDamagePoint);
                    _model.SpendUpgradePoints(ECharacterStat.Health, _countHealthPoint);
                    _model.SpendUpgradePoints(ECharacterStat.Speed, _countSpeedPoint);
                })
                .AddTo(_disposables);
            
            _view.UpgradeDamage.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GetRemainingPoints() == 0)
                        return;

                    _countDamagePoint++;
                    _view.SetDamageUpdatePoint(_countDamagePoint);
                    UpdateRemainingPoints();
                })
                .AddTo(_disposables);
            
            _view.UpgradeHealth.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GetRemainingPoints() == 0)
                        return;

                    _countHealthPoint++;
                    _view.SetHealthUpdatePoint(_countHealthPoint);
                    UpdateRemainingPoints();
                })
                .AddTo(_disposables);
            
            _view.UpgradeSpeed.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GetRemainingPoints() == 0)
                        return;

                    _countSpeedPoint++;
                    _view.SetSpeedUpdatePoint(_countSpeedPoint);
                    UpdateRemainingPoints();
                })
                .AddTo(_disposables);
        }

        private void SubscribeLocalizationText()
        {
            _model.Title
                .Subscribe(_view.UpdateTitleText)
                .AddTo(_disposables);
            
            _model.PointsLabel
                .Subscribe(_view.UpdatePointsLabel)
                .AddTo(_disposables);
            
            _model.SpeedLabel
                .Subscribe(_view.UpdateSpeedLabel)
                .AddTo(_disposables);
            
            _model.SpeedLabel
                .Subscribe(_view.UpdateSpeedLabel)
                .AddTo(_disposables);
            
            _model.DamageLabel
                .Subscribe(_view.UpdateDamageLabel)
                .AddTo(_disposables);
            
            _model.HealthLabel
                .Subscribe(_view.UpdateHealthLabel)
                .AddTo(_disposables);
            
            _model.ApplyButtonText
                .Subscribe(_view.UpdateApplyButtonText)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _disposables.Clear();
        }

        private int GetRemainingPoints()
        {
            var reservedPoints = Mathf.Max(0, _countDamagePoint - 1)
                                 + Mathf.Max(0, _countHealthPoint - 1)
                                 + Mathf.Max(0, _countSpeedPoint - 1);

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
