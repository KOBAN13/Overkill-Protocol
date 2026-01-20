using CharacterStats.Interface;
using CharacterStats.Stats;

namespace CharactersStats.Interface
{
    public interface IStatConfigProvider
    {
        TConfig GetConfig<TConfig>(ECharacterStat statType) where TConfig : class, IStatConfig;
    }
}
