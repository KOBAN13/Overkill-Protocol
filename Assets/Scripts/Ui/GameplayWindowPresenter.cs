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

        public GameplayWindowPresenter(GameplayWindowView gameplayWindowView, GameplayWindowModel gameplayWindowModel)
        {
            _view = gameplayWindowView;
            _model = gameplayWindowModel;
        }
        
        public void Initialize()
        {
            _view.OpenUpgradeWindow.OnClickAsObservable()
                .Subscribe(_ => _upgradeWindowView.gameObject.SetActive(true))
                .AddTo(_disposables);

            _model.CurrentHealth
                .Subscribe(_view.UpdatePlayerHealth)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            
        }
    }
}
