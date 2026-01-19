using Cysharp.Threading.Tasks;
using R3;

namespace Game.Stats.Interface
{
    public interface IHealthStats
    {
        ReadOnlyReactiveProperty<float> CurrentHealthPercentage { get; }
        float CurrentHealth { get; }
        Observable<float> OnCurrentValueChanged { get; }
        void SetDamage(float value);
        void ResetHealthStat();
        UniTaskVoid AddHealth(float value);
        UniTaskVoid SetHealth(float value);
    }
}