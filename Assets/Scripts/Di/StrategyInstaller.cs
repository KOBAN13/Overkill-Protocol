using Input;
using StrategyInstaller;
using Zenject;

namespace Di
{
    public class StrategyInstaller : MonoInstaller
    {
        private DiContainer _diContainer;
        
        public override void InstallBindings()
        {
            _diContainer = Container.CreateSubContainer();

            BindStrategies();
        }

        private void BindStrategies()
        {
            _diContainer.BindInterfacesAndSelfTo<MobileInputSystem>().AsSingle();
            _diContainer.BindInterfacesAndSelfTo<InputSystemPC>().AsSingle();
        }

        private void SetStrategies<TStrategy, TStrategyClient>() 
            where TStrategy : TStrategyClient
            where TStrategyClient : IStrategy
        {
            var strategy = _diContainer.Resolve<TStrategy>();
            var allClients = _diContainer.ResolveAll<IStrategyClient<TStrategyClient>>();

            foreach (var strategyClient in allClients)
            {
                strategyClient.SetStrategy(strategy);
            }
        }
    }
}