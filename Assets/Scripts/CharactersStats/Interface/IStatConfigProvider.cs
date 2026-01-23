using CharactersStats.Stats;
using Utils.Enums;

namespace CharactersStats.Interface
{
    public interface IStatConfigProvider
    {
        public TConfig GetConfig<TConfig>(EStatsOwner statsOwner, ECharacterStat statType)
            where TConfig : class, IStatConfig;
    }
}
