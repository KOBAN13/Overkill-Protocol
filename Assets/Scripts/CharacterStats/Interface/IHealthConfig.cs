namespace CharacterStats.Interface
{
    public interface IHealthConfig
    {
        float MaxValue { get; }
        float CoefficientRecoveryHealth { get; }
        float TimeRecoveryHealth { get; }
    }
}