using UnityEngine;

namespace Weapons.Scripts
{
    public interface IBulletSpawner
    {
        public void SpawnBullet(Vector3 position, Quaternion rotation , float damage);
    }
}
