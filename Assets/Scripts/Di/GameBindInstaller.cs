using Bootstrap;
using Character;
using CharacterStats.Stats;
using Enemy.Factory;
using Enemy.Pooling;
using Enemy.Walk;
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
                    var stats = new StatsCollection();
                    var configProvider = context.Container.ResolveId<IStatConfigProvider>("PlayerStats");

                    stats.SetConfigProvider(configProvider);
                    stats.AddStat(new HealthCharacter());
                    stats.AddStat(new SpeedCharacter());
                    stats.AddStat(new DamageStat());

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
