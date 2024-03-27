 using UnityEngine;
using Zenject;

namespace Installers
{
    public class DontDestroyOnLoadInstaller : MonoInstaller
    {
        [SerializeField] private GameObject soundManager;
        [SerializeField] private GameObject storeHandler;

        public override void InstallBindings()
        {
            var instance = Instantiate(soundManager);
            DontDestroyOnLoad(instance);
            Container.Bind<SoundManager>().FromComponentOn(instance).AsSingle().NonLazy();

            instance = Instantiate(storeHandler);
            DontDestroyOnLoad(storeHandler);
            Container.Bind<StoreHandler>().FromComponentOn(instance).AsSingle().NonLazy();
        }
    }
}
