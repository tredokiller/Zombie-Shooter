using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InputManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject inputManager;
        public override void InstallBindings()
        {
            var instance = Instantiate(inputManager);
            DontDestroyOnLoad(instance);
            Container.Bind<InputManager>().FromComponentOn(instance).AsSingle();
        }
    }
}