using UnityEngine;
using Weapons.AmmoBox.Scripts;
using Zenject;

namespace Managers
{
    public class PlaygroundManager : MonoBehaviour
    {
        private IWavesManager _wavesManager;
        private IAmmoBoxSpawner _ammoBoxSpawner;
        
        [Inject]
        private void Constructor(IWavesManager wavesManager , IAmmoBoxSpawner ammoBoxSpawner)
        {
            _wavesManager = wavesManager;
            _ammoBoxSpawner = ammoBoxSpawner;
        }

        private void Start()
        {
            _wavesManager.SpawnWave();
            _ammoBoxSpawner.StartSpawningAmmoBoxes();
        }
    }
}
