using CharacterStats.Stats;

namespace CharacterStats.Interface
{
    public interface IStatConfigProvider
    {
        TConfig GetConfig<TConfig>(ECharacterStat statType) where TConfig : class, IStatConfig;
    }
}
