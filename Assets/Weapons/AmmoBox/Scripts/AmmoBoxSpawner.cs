using Common.CommonScripts;
using UnityEngine;
using Zenject;

namespace Weapons.AmmoBox.Scripts
{
    public class AmmoBoxSpawner : MonoBehaviour , IAmmoBoxSpawner
    {
        [SerializeField] private GameObject ammoBoxPrefab;
        [SerializeField] private Renderer spawnZone;

        [Inject] private DiContainer _diContainer;
        
        private const float TimeBetweenSpawning = 32f;
        private const float DistanceFromGround = 15f;

        [SerializeField] private int minSpawnAmmo;
        [SerializeField] private int maxSpawnAmmo;

        private bool _canSpawnBoxes;

        public void StartSpawningAmmoBoxes()
        {
            _canSpawnBoxes = true;
            SpawnAmmoBox();
        }

        public void StopSpawningAmmoBoxes()
        {
            _canSpawnBoxes = false;
        }
        
        private void SpawnAmmoBox()
        {
            var ammoCount = Random.Range(minSpawnAmmo, maxSpawnAmmo);
            var ammoBoxHorizontalPosition = RendererTools.GetRandomPositionInRenderer(spawnZone);
            
            var ammoBox = _diContainer.InstantiatePrefabForComponent<AmmoBox>(ammoBoxPrefab, transform);

            ammoBox.AmmoInBox = ammoCount;
            ammoBox.transform.position = new Vector3(ammoBoxHorizontalPosition.x,
                spawnZone.transform.position.y + DistanceFromGround, ammoBoxHorizontalPosition.z);

            if (_canSpawnBoxes)
            {
                Timer.StartTimer(TimeBetweenSpawning , SpawnAmmoBox);
            }
        }
    }
}
