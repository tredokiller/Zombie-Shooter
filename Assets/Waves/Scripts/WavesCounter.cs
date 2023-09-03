using System;
using Common.CommonScripts;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Waves.Scripts
{
    [RequireComponent(typeof(UITransitioner))]
    public class WavesCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countTextObj;
        [SerializeField] private TextMeshProUGUI wavesDisplayTextObj;

        private const string WavesDisplayText = "WAVE ";
        private UITransitioner _uiTransitioner;
        private IWavesManager _wavesManager;

        [SerializeField] private float wavesDisplayVisibilityDuration;
        
        private int _countValue;
        public static Action OnCounterDown;



        [Inject]
        private void Constructor(IWavesManager wavesManager)
        {
            _wavesManager = wavesManager;
        }
        
        private void Awake()
        {
            _uiTransitioner = GetComponent<UITransitioner>();
            
            _countValue = WavesManager.BetweenWavesCoolDownTime;
            countTextObj.text = _countValue.ToString();
        }

        private void OnEnable()
        {
            WavesManager.OnStartedWaves += CountDown;
            WavesManager.OnSpawnedWave += DisplayWavesText;
        }
        
        private void CountDown()
        {
            if (_countValue == 0)
            {
                _countValue = WavesManager.BetweenWavesCoolDownTime;
                OnCounterDown.Invoke();
            }

            _countValue -= 1;
            countTextObj.text = _countValue.ToString();
            
            Timer.StartTimer(1 , CountDown);
        }


        private void DisplayWavesText()
        {
            wavesDisplayTextObj.text = WavesDisplayText + _wavesManager.GetCurrentWaveValue();
            _uiTransitioner.PlayTransitionFromTo();
            Timer.StartTimer(wavesDisplayVisibilityDuration ,  () => _uiTransitioner.PlayTransitionToFrom());
        }
        

        private void OnDisable()
        {
            WavesManager.OnStartedWaves -= CountDown;
            WavesManager.OnSpawnedWave -= DisplayWavesText;
        }
    }
}
