namespace StrategyInstaller
{
    public interface IStrategyClient<in TStrategy>  where TStrategy : IStrategy
    {
        void SetStrategy(TStrategy strategy);
    }
}