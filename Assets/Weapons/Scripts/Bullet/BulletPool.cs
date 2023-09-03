using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Scripts.Bullet
{
    public class BulletPool : MonoBehaviour , IBulletPool
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int poolSize = 10;

        private List<GameObject> _bullets;

        private void Start()
        {
            _bullets = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform, true);
                bullet.SetActive(false);
                _bullets.Add(bullet);
            }
        }

        public IBullet GetBullet()
        {
            foreach (GameObject bullet in _bullets)
            {
                if (!bullet.activeSelf)
                {
                    bullet.SetActive(false);
                    return bullet.GetComponent<IBullet>();
                }
            }
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(false);
            newBullet.transform.SetParent(transform);
            _bullets.Add(newBullet);

            return newBullet.GetComponent<IBullet>();;
        }
    }
}
