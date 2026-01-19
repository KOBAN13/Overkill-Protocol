namespace CharacterStats.Interface
{
    public interface IHealthConfig
    {
        float BaseValue { get; }
        float BuffHealthInPercentage { get; }
        float MaxBuffHealthInPercentage { get; }
    }
}