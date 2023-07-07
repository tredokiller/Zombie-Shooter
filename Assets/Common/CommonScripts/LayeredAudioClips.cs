using System;
using UnityEngine;

namespace Common.CommonScripts
{
    [Serializable]
    public class LayeredAudioClips
    {
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private LayerMask layerMask;

        public LayerMask LayerMask
        {
            private set => layerMask = value;
            get => layerMask;
        }

        public AudioClip[] AudioClips
        {
            private set => audioClips = value;
            get => audioClips;
        }
    }
}