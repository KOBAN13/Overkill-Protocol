namespace CharacterStats.Interface
{
    public interface IDamageConfig : IStatConfig
    {
        float BaseDamage { get; }
        float BuffDamageInPercentage { get; }
        float MaxBuffDamageInPercentage { get; }
    }
}
