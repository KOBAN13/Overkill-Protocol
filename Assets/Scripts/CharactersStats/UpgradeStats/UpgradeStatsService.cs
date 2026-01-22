using CharactersStats.Interface;
using CharactersStats.Stats;
using CharacterStats.Stats;
using Ui;
using UnityEngine;

namespace CharactersStats.UpgradeStats
{
    public class UpgradeStatsService : IUpgradeStats
    {
        private readonly StatsCollection _statsCollection;
        private readonly UpgradeWindowModel _upgradeWindowModel;

        private int _upgradePoints;

        public UpgradeStatsService(StatsCollection statsCollection, UpgradeWindowModel upgradeWindowModel)
        {
            _statsCollection = statsCollection;
            _upgradeWindowModel = upgradeWindowModel;
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
    }
}