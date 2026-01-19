using Game.Stats.Interface;

namespace CharacterStats.Interface
{
    public interface IHealth
    {
        IHealthStats HealthStats { get; }
    }
}