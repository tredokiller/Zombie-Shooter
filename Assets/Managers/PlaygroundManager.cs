using Common.CommonScripts;
using Player.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.AmmoBox.Scripts;
using Zenject;

namespace Managers
{
    public class PlaygroundManager : MonoBehaviour
    {
        private IWavesManager _wavesManager;
        private IAmmoBoxSpawner _ammoBoxSpawner;
        private IGameManager _gameManager;
        
        [SerializeField] private UITransitioner pauseButtons;
        
        [Inject]
        private void Constructor(IWavesManager wavesManager , IAmmoBoxSpawner ammoBoxSpawner , IGameManager gameManager)
        {
            _wavesManager = wavesManager;
            _ammoBoxSpawner = ammoBoxSpawner;
            _gameManager = gameManager;
        }

        private void OnEnable()
        {
            PlayerController.OnDied += Pause;
        }


        private void Start()
        {
            _wavesManager.SpawnWave();
            _ammoBoxSpawner.StartSpawningAmmoBoxes();
        }

        private void Pause()
        {
            _gameManager.SaveGame();
            pauseButtons.PlayTransitionFromTo(_gameManager.PauseGame);
        }


        public void Restart()
        {
            _gameManager.ResumeGame();
            Initiate.Fade(Scenes.Scenes.Playground.ToString(), Color.black);
        }


        public void Exit()
        {
            _gameManager.ResumeGame();
            Initiate.Fade(Scenes.Scenes.Menu.ToString(), Color.black);
        }
        
        
        private void OnDisable()
        {
            PlayerController.OnDied -= Pause;
        }
    }
}
