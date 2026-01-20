namespace CharacterStats.Interface
{
    public interface IHealthConfig : IStatConfig
    {
        float BaseValue { get; }
        float BuffHealthInPercentage { get; }
        float MaxBuffHealthInPercentage { get; }
    }
}
