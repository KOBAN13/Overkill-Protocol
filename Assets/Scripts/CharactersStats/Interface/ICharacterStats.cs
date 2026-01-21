using CharacterStats.Stats;

namespace CharactersStats.Interface
{
    public interface ICharacterStat
    {
        ECharacterStat StatType { get; }
        void Dispose();

        void UpgradeStat(int points);
    }
}