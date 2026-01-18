using Bootstrap;
using Character;
using Services.StrategyInstaller;
using UnityEngine;
using Zenject;

namespace Di
{
    public class GameBindInstaller : MonoInstaller
    {
        [SerializeField] private PlayerComponents _playerComponents;
        
        public override void InstallBindings()
        {
            BindServices();
            BindBootstrap();
            BindPlayer();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<StrategyInitializer>().AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerComponents>().FromInstance(_playerComponents).AsSingle();
            Container.BindInterfacesAndSelfTo<Movement>().AsSingle();
            Container.BindInterfacesAndSelfTo<Rotate>().AsSingle();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
        }
    }
}
