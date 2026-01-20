using R3;

namespace CharacterStats.Interface
{
    public interface IDamageStat : ICharacterStatConfig<IDamageConfig>
    {
        ReadOnlyReactiveProperty<float> CurrentDamage { get; }
        void UpdateDamage();
    }
}
