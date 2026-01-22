using CharacterStats.Stats;

namespace CharactersStats.Interface
{
    public interface IUpgradeStats
    {
        void AddUpgradePoints();
        bool UpgradeStat<TStats>(ECharacterStat characterStat, int points) where TStats : class, ICharacterStat;
    }
}