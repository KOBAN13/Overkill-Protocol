using Input;
using UnityEngine.InputSystem;
using Zenject;

namespace Di
{
    public class GameBindInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<NewInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<MobileInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystemPC>().AsSingle();
        }
    }
}