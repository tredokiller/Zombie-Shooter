
using GoogleMobileAds.Api;
using Managers;
using UnityEngine;
using Zenject;

namespace Scenes.Menu.GoogleAds
{
    public class AdManager : MonoBehaviour
    {
        private IGameManager _gameManager;
            
        [Inject]
        private void Costructor(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        
        private void Start()
        {
            MobileAds.Initialize(status => { });
        }
    }
}
