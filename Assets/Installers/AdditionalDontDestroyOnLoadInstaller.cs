using UnityEngine;
using Zenject;

namespace Installers
{
    public class AdditionalDontDestroyOnLoadInstaller : MonoInstaller
    {
        [SerializeField] private GameObject[] toInstall;
        public override void InstallBindings()
        {
            foreach (var installable in toInstall)
            {
                var instance = Instantiate(installable);
                DontDestroyOnLoad(instance);
            }
        }
    }
}
