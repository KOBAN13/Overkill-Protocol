using R3;

namespace CharactersStats.Interface
{
    public interface IDamageStat : ICharacterStatConfig<IDamageConfig>
    {
        ReadOnlyReactiveProperty<float> CurrentDamage { get; }
    }
}
