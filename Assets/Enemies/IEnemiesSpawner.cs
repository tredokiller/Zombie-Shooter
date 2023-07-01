using Common.CommonScripts;

namespace Enemies
{
    public interface IEnemiesSpawner
    {
        public void SpawnEnemy(EnemyType type , int count);
    }
}
