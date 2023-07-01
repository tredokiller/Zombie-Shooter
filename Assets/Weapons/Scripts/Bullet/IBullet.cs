using UnityEngine;

namespace Weapons.Scripts
{
    public interface IBullet
    {
        public void PushBullet(Vector3 position, Quaternion rotation , float damage);

        public void SetBulletActive(bool isActive);

        public float GetDamage();
    }
}
