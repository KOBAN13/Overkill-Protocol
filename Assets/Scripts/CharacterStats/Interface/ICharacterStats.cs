using CharacterStats.Stats;
using R3;

namespace Game.Stats.Interface
{
    public interface ICharacterStat
    {
        ECharacterStat StatType { get; }
        float MaxValue { get; }
        Observable<float> OnCurrentValueChanged { get; }
        void Dispose();
    }
}