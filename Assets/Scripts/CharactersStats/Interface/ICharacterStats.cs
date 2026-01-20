using CharacterStats.Stats;

namespace CharacterStats.Interface
{
    public interface ICharacterStat
    {
        ECharacterStat StatType { get; }
        void Dispose();

        void UpgradeStat();
    }
}