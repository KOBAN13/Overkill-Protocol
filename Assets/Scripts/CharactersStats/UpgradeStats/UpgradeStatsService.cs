using System;
using CharactersStats.Interface;
using CharactersStats.Stats;
using CharacterStats.Interface;
using CharacterStats.Stats;
using R3;
using Ui;
using UnityEngine;
using Zenject;

namespace CharactersStats.UpgradeStats
{
    public class UpgradeStatsService : IUpgradeStats, IInitializable
    {
        private readonly StatsCollection _statsCollection;
        private readonly UpgradeWindowModel _upgradeWindowModel;

        private int _upgradePoints;

        public UpgradeStatsService(StatsCollection statsCollection, UpgradeWindowModel upgradeWindowModel)
        {
            _statsCollection = statsCollection;
            _upgradeWindowModel = upgradeWindowModel;
        }
        
        public void Initialize()
        {
            _upgradeWindowModel.SpentUpgradePoints.Skip(1)
                .Subscribe(data => UpgradeStats(data.Type, data.UpgradePoints));
        }

        public void AddUpgradePoints()
        {
            _upgradePoints++;
            _upgradeWindowModel.AddUpdatePoint(_upgradePoints);
        }

        public bool UpgradeStat<TStats>(ECharacterStat characterStat, int points) where TStats : class, ICharacterStat
        {
            var countUpgradePoints = Mathf.Clamp(_upgradePoints - points, 0, int.MaxValue);
            
            if (countUpgradePoints == 0)
                return false;
            
            var stat = _statsCollection.GetStat<TStats>(characterStat);
            
            stat.UpgradeStat(points);
            
            return true;
        }

        private void UpgradeStats(ECharacterStat characterStat, int points)
        {
            switch (characterStat)
            {
                case ECharacterStat.Health:
                    UpgradeStat<IHealthStat>(characterStat, points);
                    break;

                case ECharacterStat.Damage:
                    UpgradeStat<IDamageStat>(characterStat, points);
                    break;
                
                case ECharacterStat.Speed:
                    UpgradeStat<ISpeedStat>(characterStat, points);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterStat), characterStat, null);
            }
        }
    }
}