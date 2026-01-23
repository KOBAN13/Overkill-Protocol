namespace CharactersStats.Interface
{
    public interface ISpeedConfig : IStatConfig
    {
        float BaseSpeed { get; }
        float BuffSpeedInPercentage { get; }
        float MaxBuffSpeedInPercentage { get; }
    }
}
