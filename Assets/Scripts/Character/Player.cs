using Input.Interface;
using StrategyInstaller;

namespace Character
{
    public class Player : IStrategyClient<IInputSystem>
    {
        private IInputSystem _inputSystem;
        
        public void SetStrategy(IInputSystem strategy)
        {
            _inputSystem = strategy;
        }
    }
}