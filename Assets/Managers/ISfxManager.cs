using UnityEngine;

namespace Managers
{
    public interface ISfxManager
    {
        public void MakeSound(AudioClip sound , float volume = 1);
        public void MakeMusic(AudioClip music);
    }
}
