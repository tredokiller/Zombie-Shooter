using Player.Scripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerControllerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerControllerObj;
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentOn(playerControllerObj).AsCached();
        }
    }
}