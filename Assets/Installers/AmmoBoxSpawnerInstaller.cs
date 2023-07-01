using UnityEngine;
using Weapons.AmmoBox.Scripts;
using Zenject;

namespace Installers
{
    public class AmmoBoxSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject ammoBoxSpawnerObj;
        public override void InstallBindings()
        {
            Container.Bind<IAmmoBoxSpawner>().To<AmmoBoxSpawner>().FromComponentOn(ammoBoxSpawnerObj).AsSingle();
        }
    }
}
