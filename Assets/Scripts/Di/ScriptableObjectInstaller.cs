using Character.Config;
using Enemy.Config;
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
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeaponConfig>().FromScriptableObject(_weaponConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerParameters>().FromScriptableObject(_playerParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnParameters>().FromScriptableObject(_enemySpawnParameters).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyParameters>().FromScriptableObject(_enemyParameters).AsSingle();
        }
    }
}