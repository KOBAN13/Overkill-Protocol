using System;
using Input;
using StrategyInstaller;
using Zenject;

namespace Services.StrategyInstaller
{
    //TODO: Подумать над интерфейсамми мб стоит обоертку намутить между инпутами, если останется время
    public class StrategyInitializer : IStrategyInitializer, ITickable, IDisposable
    {
        private readonly DiContainer _diContainer;
        private IStrategy _currentStrategy;

        public StrategyInitializer(DiContainer diContainer)
        {
            _diContainer = diContainer.CreateSubContainer();
            
            BindStrategies();
        }
        
        public void SetStrategies<TStrategy, TStrategyClient>() 
            where TStrategy : TStrategyClient
            where TStrategyClient : IStrategy
        {
            var strategy = _diContainer.Resolve<TStrategy>();
            _currentStrategy = strategy;
            var allClients = _diContainer.ResolveAll<IStrategyClient<TStrategyClient>>();

            foreach (var strategyClient in allClients)
            {
                strategyClient.SetStrategy(strategy);
            }

            if (strategy is IInitializable initializable)
            {
                initializable.Initialize();
            }
        }

        public void Tick()
        {
            if (_currentStrategy is ITickable tickable)
            {
                tickable.Tick();
            }
        }

        public void Dispose()
        {
            if (_currentStrategy is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void BindStrategies()
        {
            _diContainer.BindInterfacesAndSelfTo<NewInputSystem>().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<MobileInputSystem>().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<InputSystemPC>().AsSingle();
        }
    }
}
