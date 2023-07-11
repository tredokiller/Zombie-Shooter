using Common.CommonScripts;
using DG.Tweening;
using UnityEngine;

namespace Weapons.Scripts.Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour , IBullet
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float liveTime = 3f;

        private Rigidbody _rb;
        private float _damage;

        private bool _canDisableBulletByTimer;
        private Tween _timer;

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
            
            StartTimer();
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

        private void StartTimer()
        {
            if (_timer != null)
            {
                _timer.Kill();
            }
            _timer = Timer.StartTimer(liveTime  , (() => SetBulletActive(false)));
        }
    }
}
