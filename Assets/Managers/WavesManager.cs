using System;
using Common.CommonScripts;
using Enemies;
using Enemies.Waves;
using UnityEngine;
using Waves.Scripts;
using Zenject;

namespace Managers
{
    public class WavesManager : MonoBehaviour , IWavesManager
    {
        [SerializeField] private WaveData[] waveDates;

        private int _currentWaveIndex;

        private int _currentDefaultZombiesCount;
        private int _currentTankZombiesCount;
        private int _currentLyingZombiesCount;

        private const float EndlessWavesMultiplier = 1.05f;
        public const int BetweenWavesCoolDownTime = 45;

        private IEnemiesSpawner _enemiesSpawner;

        public static Action OnSpawnedWave;
        public static Action OnStartedWaves;


        [Inject]
        private void Constructor(IEnemiesSpawner enemiesSpawner)
        {
            _enemiesSpawner = enemiesSpawner;
        }
        
        private void Awake()
        {
            _currentWaveIndex = 0;
        }

        private void OnEnable()
        {
            WavesCounter.OnCounterDown += SpawnWave;
        }

        public void SpawnWave()
        {
            if (_currentWaveIndex == 0)
            {
                OnStartedWaves.Invoke();  
            }
            
            SetCurrentEnemiesCount();
            
            _currentWaveIndex += 1;
            _enemiesSpawner.SpawnEnemy(EnemyType.DefaultZombie, _currentDefaultZombiesCount);
            _enemiesSpawner.SpawnEnemy(EnemyType.LyingZombie, _currentLyingZombiesCount);
            
            OnSpawnedWave?.Invoke();
        }

        public int GetCurrentWaveValue()
        {
            return _currentWaveIndex;
        }
        
        private void SetCurrentEnemiesCount()
        {
            if (_currentWaveIndex < waveDates.Length)
            {
                _currentDefaultZombiesCount = waveDates[_currentWaveIndex].defaultZombiesCount;
                _currentLyingZombiesCount = waveDates[_currentWaveIndex].lyingZombiesCount;
                _currentTankZombiesCount = waveDates[_currentWaveIndex].tankZombiesCount;
            }
            else
            {
                _currentDefaultZombiesCount = (int)(_currentDefaultZombiesCount * EndlessWavesMultiplier);
                _currentLyingZombiesCount = (int)(_currentLyingZombiesCount * EndlessWavesMultiplier);
                _currentTankZombiesCount = (int)(_currentTankZombiesCount * EndlessWavesMultiplier);
            }  
        }

        private void OnDisable()
        {
            WavesCounter.OnCounterDown -= SpawnWave;
        }
    }
}
