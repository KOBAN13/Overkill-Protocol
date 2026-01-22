using Bootstrap;
using Character;
using CharactersStats.Builder;
using CharactersStats.Interface;
using CharactersStats.Stats;
using Enemy.Factory;
using Enemy.Pooling;
using CharactersStats.UpgradeStats;
using Enemy;
using Services.Spawners;
using Services.StrategyInstaller;
using Ui;
using UnityEngine;
using UnityEngine.Pool;
using Utils.Enums;
using Weapon.WeaponType;
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
            BindUi();
        }

        private void BindUi()
        {
            Container.BindInterfacesAndSelfTo<UpgradeWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeWindowModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameplayWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayWindowModel>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<StrategyInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<PointsCamera>().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomObjectPool<HumanoidEnemy>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomObjectPool<ParticleSystem>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<Pistol>().FromComponentInHierarchy().AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerComponents>().FromInstance(_playerComponents).AsSingle();
            Container.BindInterfacesAndSelfTo<Movement>().AsSingle();
            Container.BindInterfacesAndSelfTo<Rotate>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatsCollection>()
                .FromMethod(context =>
                {
                    var builder = new CharacterStatsBuilder();
                    var configProvider = context.Container.Resolve<IStatConfigProvider>();
                    
                    var stats = builder
                        .AddConfigs(configProvider)
                        .AddSpeedStat(EStatsOwner.Player)
                        .AddHealthStat(EStatsOwner.Player)
                        .AddDamageStat(EStatsOwner.Player)
                        .Build();

                    return stats;
                })
                .AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeStatsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
        }
    }
}
