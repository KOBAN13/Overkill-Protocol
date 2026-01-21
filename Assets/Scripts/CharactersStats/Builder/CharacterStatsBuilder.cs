using CharactersStats.Interface;
using CharactersStats.Stats;
using CharacterStats.Stats;
using Utils.Enums;

namespace CharactersStats.Builder
{
    public class CharacterStatsBuilder
    {
        private readonly StatsCollection _characterStats = new();

        public CharacterStatsBuilder AddConfigs(IStatConfigProvider cfgProvider)
        {
            _characterStats.SetConfigProvider(cfgProvider);
            return this;
        }
        
        public CharacterStatsBuilder AddHealthStat(EStatsOwner statsOwner)
        {
            _characterStats.AddStat(statsOwner, new HealthCharacter());
            return this;
        }

        public CharacterStatsBuilder AddSpeedStat(EStatsOwner statsOwner)
        {
            _characterStats.AddStat(statsOwner, new SpeedCharacter());
            return this;
        }

        public CharacterStatsBuilder AddDamageStat(EStatsOwner statsOwner)
        {
            _characterStats.AddStat(statsOwner, new DamageStat());
            return this;
        }

        public StatsCollection Build()
        {
            return _characterStats;
        }
    }
}
