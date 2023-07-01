using UnityEngine;

namespace Managers
{
    public interface ISfxManager
    {
        public void MakeSound(AudioClip sound);
        public void MakeMusic(AudioClip music);
    }
}
