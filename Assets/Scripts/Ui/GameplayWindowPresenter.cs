using R3;
using Zenject;

namespace Ui
{
    public class GameplayWindowPresenter : PresenterBase, IInitializable
    {
        private readonly GameplayWindowModel _model;
        private readonly GameplayWindowView _view;
        private readonly UpgradeWindowView _upgradeWindowView;
        
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
            BindClick(_view.OpenUpgradeWindow.OnClickAsObservable(), () =>
            {
                _upgradeWindowView.CanvasGroup.alpha = 1;
                _view.CanvasGroup.alpha = 0;
            });

            Bind(_model.CurrentHealth, _view.UpdatePlayerHealth);
            Bind(_model.HealthTitle, _view.UpdateHealthTitle);
            Bind(_model.UpgradeWindowText, _view.UpdateTextOpenUpgradeWindow);
        }
    }
}
