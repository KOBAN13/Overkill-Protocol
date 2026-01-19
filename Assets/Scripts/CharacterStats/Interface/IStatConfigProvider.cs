using CharacterStats.Stats;

namespace CharacterStats.Interface
{
    public interface IStatConfigProvider
    {
        IStatConfig GetConfig(ECharacterStat statType);
    }
}
