using R3;

namespace CharactersStats.Interface
{
    public interface ISpeedStat : ICharacterStatConfig<ISpeedConfig>
    {
        ReadOnlyReactiveProperty<float> CurrentSpeed { get; }
    }
}