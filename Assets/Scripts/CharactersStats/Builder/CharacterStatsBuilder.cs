using CharacterStats.Stats;

namespace CharactersStats.Builder
{
    public class CharacterStatsBuilder
    {
        private readonly StatsCollection _characterStats = new();

        public CharacterStatsBuilder AddHealthStat()
        {
            _characterStats.AddStat(new HealthCharacter());
            return this;
        }

        public CharacterStatsBuilder AddSpeedStat()
        {
            _characterStats.AddStat(new SpeedCharacter());
            return this;
        }

        public CharacterStatsBuilder AddDamageStat()
        {
            _characterStats.AddStat(new DamageStat());
            return this;
        }

        public StatsCollection Build()
        {
            return _characterStats;
        }
    }
}
