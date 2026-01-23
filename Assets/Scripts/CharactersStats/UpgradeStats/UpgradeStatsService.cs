using CharactersStats.Interface;
using CharactersStats.Stats;
using R3;
using Ui;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace CharactersStats.UpgradeStats
{
    public class UpgradeStatsService : IUpgradeStats, IInitializable
    {
        private readonly StatsCollection _statsCollection;
        private readonly UpgradeWindowModel _upgradeWindowModel;

        private int _upgradePoints;
        
        private readonly Dictionary<ECharacterStat, int> _spentPoints = new();

        public UpgradeStatsService(StatsCollection statsCollection, UpgradeWindowModel upgradeWindowModel)
        {
            _statsCollection = statsCollection;
            _upgradeWindowModel = upgradeWindowModel;
        }
        
        public void Initialize()
        {
            _upgradeWindowModel.SpentUpgradePoints
                .Subscribe(data => UpgradeStats(data.Type, data.UpgradePoints));
        }

        public void AddUpgradePoints()
        {
            _upgradePoints++;
            _upgradeWindowModel.AddUpdatePoint(_upgradePoints);
        }

        public bool UpgradeStat<TStats>(ECharacterStat characterStat, int points) where TStats : class, ICharacterStat
        {
            if (points <= 0)
                return false;

            if (_upgradePoints < points)
                return false;

            var currentSpent = _spentPoints.GetValueOrDefault(characterStat, 0);
            var totalSpent = currentSpent + points;
            _spentPoints[characterStat] = totalSpent;
            
            var stat = _statsCollection.GetStat<TStats>(characterStat);
            
            stat.UpgradeStat(totalSpent);

            _upgradePoints = Mathf.Max(0, _upgradePoints - points);
            _upgradeWindowModel.AddUpdatePoint(_upgradePoints);
            
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
            }
        }
    }
}
