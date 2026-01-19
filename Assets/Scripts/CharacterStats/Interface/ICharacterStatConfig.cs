using CharacterStats.Interface;

namespace Game.Stats.Interface
{
    public interface ICharacterStatConfig<in T> : ICharacterStat where T : IStatConfig
    {
        void Initialize(T config);
    }
}