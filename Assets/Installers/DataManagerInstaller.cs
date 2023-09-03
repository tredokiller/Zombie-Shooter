using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DataManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject dataManager;
        public override void InstallBindings()
        {
            var instance = Instantiate(dataManager);
            DontDestroyOnLoad(instance);
            Container.Bind<IDataManager>().To<DataManager>().FromComponentOn(instance).AsSingle().NonLazy();
        }
    }
}
