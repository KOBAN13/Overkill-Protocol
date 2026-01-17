using Bootstrap;
using Input;
using Zenject;

namespace Di
{
    public class GameBindInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindBootstrap();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<NewInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystemPC>().AsSingle();
            Container.BindInterfacesAndSelfTo<MobileInputSystem>().AsSingle();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
        }
    }
}
