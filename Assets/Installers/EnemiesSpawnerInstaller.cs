using Enemies;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemiesSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject enemiesSpawnerObj;

        public override void InstallBindings()
        {
            Container.Bind<IEnemiesSpawner>().To<EnemiesSpawner>().FromComponentOn(enemiesSpawnerObj).AsSingle();
        }
    }
}
