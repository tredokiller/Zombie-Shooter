using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class WavesManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject wavesManagerObj;
        public override void InstallBindings()
        {
            Container.Bind<IWavesManager>().To<WavesManager>().FromComponentOn(wavesManagerObj).AsSingle();
        }
    }
}
