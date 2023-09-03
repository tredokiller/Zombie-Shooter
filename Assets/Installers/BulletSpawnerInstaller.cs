using UnityEngine;
using Weapons.Scripts;
using Weapons.Scripts.Bullet;
using Zenject;

namespace Installers
{
    public class BulletSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject bulletManager;
        public override void InstallBindings()
        {
            Container.Bind<IBulletSpawner>().To<BulletSpawner>().FromComponentOn(bulletManager).AsSingle();
        }
    }
}
