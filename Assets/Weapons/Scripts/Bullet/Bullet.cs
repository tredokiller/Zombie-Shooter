using Common.CommonScripts;
using UnityEngine;

namespace Weapons.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour , IBullet
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float liveTime = 3f;

        private Rigidbody _rb;
        private float _damage;

        private bool _canDisableBulletByTimer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void PushBullet(Vector3 position, Quaternion rotation , float damage)
        {
            transform.position = position;
            transform.rotation = rotation;

            _damage = damage;
            
            _rb.velocity = transform.forward * speed;
            
            _canDisableBulletByTimer = true;
            
            Timer.StartTimer(liveTime  , DisableBulletByTimer);
        }


        private void DisableBulletByTimer()
        {
            if (_canDisableBulletByTimer)
            {
                SetBulletActive(false);
            }
        }
        
        public void SetBulletActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            _canDisableBulletByTimer = false;
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}
