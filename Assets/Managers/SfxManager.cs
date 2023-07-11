using System;
using Common.CommonScripts;
using UnityEngine;

namespace Managers
{
    public class SfxManager : MonoBehaviour , ISfxManager
    {
        [SerializeField] private GameObject sounds;
        [SerializeField] private GameObject music;

        private AudioSource[] _soundSources;
        private AudioSource[] _musicSources;

        private void Awake()
        {
            gameObject.SetActive(true);
            sounds.SetActive(true);
        }

        private void Start()
        {
            _soundSources = sounds.GetComponentsInChildren<AudioSource>();
            _musicSources = music.GetComponentsInChildren<AudioSource>();
        }

        public void MakeSound(AudioClip sound , float volume = 1)
        {
            gameObject.SetActive(true);
            sounds.SetActive(true);
            _soundSources = sounds.GetComponentsInChildren<AudioSource>();
            MakeSfx(_soundSources, sound , volume);
        }
        
        public void MakeMusic(AudioClip sfxMusic)
        {
            MakeSfx(_musicSources, sfxMusic);
        }

        private void MakeSfx(AudioSource[] players , AudioClip clip , float volume = 1)
        {
            foreach (var soundPlayer in players)
            {
                soundPlayer.gameObject.SetActive(true);
                if (!soundPlayer.isPlaying)
                {
                    soundPlayer.volume = volume;
                    soundPlayer.enabled = true;
                    soundPlayer.clip = clip;
                    soundPlayer.Play();
                    break;
                }
            }
        }
        
    }
}
