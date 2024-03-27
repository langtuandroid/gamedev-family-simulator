using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameplayHandler gameplayHandler;

        public override void InstallBindings()
        {
            Container.Bind<GameplayHandler>().FromInstance(gameplayHandler).AsSingle().NonLazy();
        }
    }
}
