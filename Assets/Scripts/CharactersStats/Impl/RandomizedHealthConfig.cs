using CharacterStats.Interface;
using CharacterStats.Stats;

namespace CharactersStats.Impl
{
    public sealed class RandomizedHealthConfig : IHealthConfig
    {
        public ECharacterStat StatType => ECharacterStat.Health;
        public float BaseValue { get; }
        public float BuffHealthInPercentage { get; }
        public float MaxBuffHealthInPercentage { get; }

        public RandomizedHealthConfig(IHealthConfig baseConfig, float randomizedBaseValue)
        {
            BaseValue = randomizedBaseValue;
            BuffHealthInPercentage = baseConfig.BuffHealthInPercentage;
            MaxBuffHealthInPercentage = baseConfig.MaxBuffHealthInPercentage;
        }
    }
}
