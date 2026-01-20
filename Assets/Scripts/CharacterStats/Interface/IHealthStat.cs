using R3;

namespace CharacterStats.Interface
{
    public interface IHealthStat : ICharacterStatConfig<IHealthConfig>
    {
        ReadOnlyReactiveProperty<float> CurrentHealthPercentage { get; }
        float CurrentHealth { get; }
        void SetDamage(float value);
        void ResetHealthStat();
        void UpgradeStat();
    }
}
