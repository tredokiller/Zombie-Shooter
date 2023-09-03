using UnityEngine;

namespace Weapons.Scripts.Bullet
{
    [RequireComponent(typeof(BulletPool))]
    public class BulletSpawner : MonoBehaviour , IBulletSpawner
    {
        private IBulletPool _bulletPool;
        private void Awake()
        {
            _bulletPool = GetComponent<BulletPool>();
        }


        public void SpawnBullet(Vector3 position, Quaternion rotation , float damage)
        {
            var bullet = _bulletPool.GetBullet();
            bullet.PushBullet(position, rotation , damage);
            bullet.SetBulletActive(true);
        }
        
    }
}
