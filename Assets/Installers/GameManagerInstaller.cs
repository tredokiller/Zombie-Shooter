using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gameManager;
        public override void InstallBindings()
        {
            var instance = Instantiate(gameManager);
            DontDestroyOnLoad(instance);
            Container.Bind<IGameManager>().To<GameManager>().FromComponentOn(instance).AsSingle();
        }
    }
}