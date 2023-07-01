using Player.Scripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MouseInteractionInstaller : MonoInstaller
    {
        [SerializeField] private GameObject mouseInteraction;
        public override void InstallBindings()
        {
            Container.Bind<IMouseInteraction>().To<MouseInteraction>().FromComponentOn(mouseInteraction).AsSingle();
        }
    }
}
