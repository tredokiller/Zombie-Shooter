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

        public void MakeSound(AudioClip sound)
        {
            gameObject.SetActive(true);
            sounds.SetActive(true);
            _soundSources = sounds.GetComponentsInChildren<AudioSource>();
            MakeSfx(_soundSources, sound);
        }
        
        public void MakeMusic(AudioClip sfxMusic)
        {
            MakeSfx(_musicSources, sfxMusic);
        }

        private void MakeSfx(AudioSource[] players , AudioClip clip)
        {
            foreach (var soundPlayer in players)
            {
                soundPlayer.gameObject.SetActive(true);
                if (!soundPlayer.isPlaying)
                {
                    soundPlayer.enabled = true;
                    soundPlayer.clip = clip;
                    soundPlayer.Play();
                    break;
                }
            }
        }
        
    }
}
