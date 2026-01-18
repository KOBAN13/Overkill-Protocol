using StrategyInstaller;

namespace Services.StrategyInstaller
{
    public interface IStrategyInitializer
    {
        public void SetStrategies<TStrategy, TStrategyClient>()
            where TStrategy : TStrategyClient
            where TStrategyClient : IStrategy;
    }
}