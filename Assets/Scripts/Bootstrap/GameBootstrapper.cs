using Input;
using Input.Interface;
using Services.StrategyInstaller;
using Zenject;

namespace Bootstrap
{
    public class GameBootstrapper : IInitializable
    {
        private readonly IStrategyInitializer _strategyInitializer;

        public GameBootstrapper(IStrategyInitializer strategyInitializer)
        {
            _strategyInitializer = strategyInitializer;
        }

        public void Initialize()
        {
            InitializeStrategy();
        }

        private void InitializeStrategy()
        {
#if UNITY_ANDROID || UNITY_IOS
            _strategyInitializer.SetStrategies<MobileInputSystem, IInputSystem>();
#else
    _strategyInitializer.SetStrategies<InputSystemPC, IInputSystem>();
#endif
        }
    }
}
