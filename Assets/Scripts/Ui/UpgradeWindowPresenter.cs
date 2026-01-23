using System;
using CharacterStats.Stats;
using R3;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class UpgradeWindowPresenter : IInitializable, IDisposable
    {
        private const int BaseUpgradeLevel = 1;

        private readonly UpgradeWindowModel _model;
        private readonly UpgradeWindowView _view;

        private readonly CompositeDisposable _disposables = new();

        private int _countHealthPoint;
        private int _countDamagePoint;
        private int _countSpeedPoint;
        
        private int _totalUpgradePoints;
        
        private int _appliedHealthPoint;
        private int _appliedDamagePoint;
        private int _appliedSpeedPoint;
        
        public UpgradeWindowPresenter(UpgradeWindowModel model, UpgradeWindowView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            SubscribeLocalizationText();
            InitializeCounters();
            
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
                .Subscribe(_ => _view.CanvasGroup.alpha = 0)
                .AddTo(_disposables);
            
            _view.Apply.OnClickAsObservable()
                .Subscribe(_ =>
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
                    
                    _view.gameObject.SetActive(false);
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
        
        public void Dispose()
        {
            _disposables.Dispose();
            _disposables.Clear();
        }
    }
}
