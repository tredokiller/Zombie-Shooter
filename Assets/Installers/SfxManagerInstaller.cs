using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SfxManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject sfxManager;
        public override void InstallBindings()
        {
            Container.Bind<ISfxManager>().To<SfxManager>().FromComponentOn(sfxManager).AsSingle();
        }
    }
}