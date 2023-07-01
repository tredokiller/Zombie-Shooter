using Common.CommonScripts;
using UnityEngine;

namespace Enemies
{
    public interface IEnemiesPool
    {
        public GameObject GetEnemy(EnemyType enemyType);
    }
}