using Game.Stats.Interface;

namespace CharacterStats.Interface
{
    public interface ICharacterStatConfig : ICharacterStat
    {
        void Initialize(IStatConfig config);
    }
}
