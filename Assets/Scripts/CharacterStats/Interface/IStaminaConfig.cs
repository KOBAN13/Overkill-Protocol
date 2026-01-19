namespace CharacterStats.Interface
{
    public interface IStaminaConfig
    {
        float BaseSpeed { get; }
        float BuffSpeedInPercentage { get; }
        float MaxBuffSpeedInPercentage { get; }
    }
}