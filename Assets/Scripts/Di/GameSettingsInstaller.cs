using Character.Config;
using CharactersStats.Impl;
using Enemy.Config;
using Localization.Configs;
using Services.Config;
using UnityEngine;
using Weapon.Configs;
using Zenject;

namespace Di
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private PlayerParameters _playerParameters;
        [SerializeField] private EnemySpawnParameters _enemySpawnParameters;
        [SerializeField] private EnemyParameters _enemyParameters;
        [SerializeField] private StatConfigCollection _statsConfig;
        [SerializeField] private UpgradeWindowViewTexts _upgradeWindowViewTexts;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeaponConfig>().FromScriptableObject(_weaponConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerParameters>().FromScriptableObject(_playerParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnParameters>().FromScriptableObject(_enemySpawnParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyParameters>().FromScriptableObject(_enemyParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowViewTexts>().FromScriptableObject(_upgradeWindowViewTexts).AsSingle();

            Container.BindInterfacesAndSelfTo<StatConfigCollection>()
                .FromScriptableObject(_statsConfig)
                .AsSingle();
        }
    }
}
