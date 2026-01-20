using R3;

namespace CharacterStats.Interface
{
    public interface ISpeedStat : ICharacterStatConfig<ISpeedConfig>
    {
        ReadOnlyReactiveProperty<float> CurrentSpeed { get; }
        void UpdateSpeed();
    }
}