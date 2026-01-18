using Character.Config;
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
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeaponConfig>().FromScriptableObject(_weaponConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerParameters>().FromScriptableObject(_playerParameters).AsSingle();
        }
    }
}