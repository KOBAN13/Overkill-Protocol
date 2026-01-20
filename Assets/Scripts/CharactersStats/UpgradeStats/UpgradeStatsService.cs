using CharactersStats.Interface;
using CharacterStats.Stats;
using CharacterStats.Interface;
using UnityEngine;

namespace CharactersStats.UpgradeStats
{
    public class UpgradeStatsService : IUpgradeStats
    {
        private readonly StatsCollection _statsCollection;

        private int _upgradePoints;

        public UpgradeStatsService(StatsCollection statsCollection)
        {
            _statsCollection = statsCollection;
        }

        public void AddUpgradePoints()
        {
            _upgradePoints++;
        }

        public bool UpgradeStat<TStats>(ECharacterStat characterStat) where TStats : class, ICharacterStat
        {
            var countUpgradePoints = Mathf.Clamp(_upgradePoints - 1, 0, 100);
            
            if (countUpgradePoints == 0)
                return false;
            
            var stat = _statsCollection.GetStat<TStats>(characterStat);
            
            stat.UpgradeStat();
            
            return true;
        }
    }
}