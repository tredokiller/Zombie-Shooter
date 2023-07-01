using UnityEngine;

namespace Enemies.Waves
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Waves/WaveData")]
    public class WaveData : ScriptableObject
    {
        public int defaultZombiesCount = 10;
        public int tankZombiesCount = 0;
        public int lyingZombiesCount = 0;
    }
}
