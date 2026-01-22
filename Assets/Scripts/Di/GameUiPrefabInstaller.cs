using Ui;
using UnityEngine;
using Zenject;

namespace Di
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameUiPrefabInstaller), fileName = nameof(GameUiPrefabInstaller))]
    public class GameUiPrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas _canvasView;

        [SerializeField] private UpgradeWindowView _upgradeWindowView;
        
        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(_canvasView);
            var canvasTransform = canvasView.transform;
            
            Container.BindInterfacesAndSelfTo<UpgradeWindowView>()
                .FromComponentInNewPrefab(_upgradeWindowView)
                .UnderTransform(canvasTransform).AsSingle();
        }
    }
}
