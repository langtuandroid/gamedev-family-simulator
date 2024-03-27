using UnityEngine;
using Zenject;

namespace Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuHandler mainMenuHandler;
        public override void InstallBindings()
        {
            Container.Bind<MainMenuHandler>().FromInstance(mainMenuHandler).AsSingle();
        }
    }
}
