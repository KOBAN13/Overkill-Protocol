namespace CharactersStats.Interface
{
    public interface ICharacterStatConfig<in TConfig> : ICharacterStat
        where TConfig : IStatConfig
    {
        void Initialize(TConfig config);
    }
}
