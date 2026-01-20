using CharacterStats.Interface;
using CharacterStats.Stats;

namespace CharactersStats.Interface
{
    public interface IUpgradeStats
    {
        void AddUpgradePoints();
        bool UpgradeStat<TStats>(ECharacterStat characterStat) where TStats : class, ICharacterStat;
    }
}