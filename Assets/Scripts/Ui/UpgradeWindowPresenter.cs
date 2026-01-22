using System;
using R3;
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
        
        public UpgradeWindowPresenter(UpgradeWindowModel model, UpgradeWindowView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            SubscribeLocalizationText();
            
            _model.AddUpgradePoints.
                Subscribe(_view.SetCountUpgradePoint)
                .AddTo(_disposables);

            SubscribeButtons();
        }

        private void SubscribeButtons()
        {
            _view.CloseWindow
                .OnClickAsObservable()
                .Subscribe(_ => _view.gameObject.SetActive(false));
            
            _view.UpgradeDamage.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _countDamagePoint++;
                    _view.SetDamageUpdatePoint(_countDamagePoint);
                })
                .AddTo(_disposables);
            
            _view.UpgradeHealth.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _countHealthPoint++;
                    _view.SetHealthUpdatePoint(_countHealthPoint);
                })
                .AddTo(_disposables);
            
            _view.UpgradeSpeed.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _countSpeedPoint++;
                    _view.SetSpeedUpdatePoint(_countSpeedPoint);
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
            
        }
    }
}