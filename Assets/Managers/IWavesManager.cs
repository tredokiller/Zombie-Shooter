using UnityEngine;

namespace Managers
{
    public interface IWavesManager
    {
        public void SpawnWave();

        public int GetCurrentWaveValue();
    }
}
