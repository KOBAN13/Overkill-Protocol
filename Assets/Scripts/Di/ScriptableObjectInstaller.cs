using Character;
using Character.Config;
using CharactersStats.Impl;
using Enemy.Config;
using Enemy.Factory;
using Services.Config;
using UnityEngine;
using Weapon.Configs;
using Zenject;

namespace Di
{
    [CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
    public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private PlayerParameters _playerParameters;
        [SerializeField] private EnemySpawnParameters _enemySpawnParameters;
        [SerializeField] private EnemyParameters _enemyParameters;
        [SerializeField] private StatConfigCollection _enemyStatConfigProvider;
        [SerializeField] private StatConfigCollection _playerStatConfigProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeaponConfig>().FromScriptableObject(_weaponConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerParameters>().FromScriptableObject(_playerParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnParameters>().FromScriptableObject(_enemySpawnParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyParameters>().FromScriptableObject(_enemyParameters).AsSingle();

            Container.BindInterfacesAndSelfTo<StatConfigCollection>()
                .FromScriptableObject(_playerStatConfigProvider)
                .AsCached()
                .WhenInjectedInto<Player>();
            
            Container.BindInterfacesAndSelfTo<StatConfigCollection>()
                .FromScriptableObject(_enemyStatConfigProvider)
                .AsCached()
                .WhenInjectedInto<EnemyFactory>();
        }
    }
}
