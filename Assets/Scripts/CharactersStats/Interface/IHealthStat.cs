using R3;

namespace CharactersStats.Interface
{
    public interface IHealthStat : ICharacterStatConfig<IHealthConfig>
    {
        ReadOnlyReactiveProperty<float> OnCurrentValueChanged { get; }
        ReadOnlyReactiveProperty<float> CurrentHealthPercentage { get; }
        float CurrentHealth { get; }
        void SetDamage(float value);
        void ResetHealthStat();
    }
}
