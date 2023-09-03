using Common.CommonScripts;
using Common.CommonScripts.Interfaces;
using Player.Scripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TargetInstaller : MonoInstaller
    {
        [SerializeField] private GameObject targetObj;
        public override void InstallBindings()
        {
            Container.Bind<ITarget>().To<PlayerController>().FromComponentOn(targetObj).AsCached();
        }
    }
}
