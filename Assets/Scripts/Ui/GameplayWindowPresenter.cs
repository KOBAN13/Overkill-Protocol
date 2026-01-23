using System;
using R3;
using Zenject;

namespace Ui
{
    public class GameplayWindowPresenter : IInitializable, IDisposable
    {
        private readonly GameplayWindowModel _model;
        private readonly GameplayWindowView _view;
        private readonly UpgradeWindowView _upgradeWindowView;
        
        private readonly CompositeDisposable _disposables = new();

        public GameplayWindowPresenter(
            GameplayWindowView gameplayWindowView,
            GameplayWindowModel gameplayWindowModel,
            UpgradeWindowView upgradeWindowView
        )
        {
            _view = gameplayWindowView;
            _model = gameplayWindowModel;
            _upgradeWindowView = upgradeWindowView;
        }
        
        public void Initialize()
        {
            _view.OpenUpgradeWindow.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _upgradeWindowView.CanvasGroup.alpha = 1;
                    _view.CanvasGroup.alpha = 0;
                })
                .AddTo(_disposables);

            _model.CurrentHealth
                .Subscribe(_view.UpdatePlayerHealth)
                .AddTo(_disposables);
            
            _model.HealthTitle
                .Subscribe(_view.UpdateHealthTitle)
                .AddTo(_disposables);
            
            _model.UpgradeWindowText
                .Subscribe(_view.UpdateTextOpenUpgradeWindow)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _disposables.Clear();
        }
    }
}
