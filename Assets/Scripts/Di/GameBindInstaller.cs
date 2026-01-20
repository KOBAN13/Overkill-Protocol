using Bootstrap;
using Character;
using CharactersStats.Builder;
using CharactersStats.Interface;
using CharacterStats.Stats;
using Enemy.Factory;
using Enemy.Pooling;
using CharactersStats.UpgradeStats;
using CharacterStats.Interface;
using Services.Spawners;
using Services.StrategyInstaller;
using UnityEngine;
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
            BindEnemy();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<StrategyInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<PointsCamera>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<Pistol>().FromComponentInHierarchy().AsSingle();
        }

        private void BindEnemy()
        {
            //Container.BindInterfacesAndSelfTo<EnemyWalk>().AsTransient();
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
                    var configProvider = context.Container.ResolveId<IStatConfigProvider>("PlayerStats");

                    var stats = builder
                        .AddSpeedStat()
                        .AddHealthStat()
                        .AddDamageStat()
                        .Build();
                    
                    stats.SetConfigProvider(configProvider);

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
