using System;
using Common.CommonScripts;
using Managers;
using UnityEngine;
using Zenject;

namespace Weapons.Scripts.WeaponBase
{
    public class Weapon : MonoBehaviour , IWeapon
    {
        [Header("ForIK")] 
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        
        [SerializeField] private WeaponData weaponData;

        private int _currentMagazineAmmo;
        private int _ammo;

        private bool _canShoot;
        
        private const float SmoothRotationTime = 10f; 
        private const float SmoothPositionTime = 100f; 
        
        private ISfxManager _sfxManager;
        private IBulletSpawner _bulletSpawner;

        private bool _isReloading;
        
        public static Action OnShot;
        public static Action OnReload;
        public static Action OnReloaded;
        public static Action OnAddedAmmo;

        [Inject]
        private void Constructor(ISfxManager sfxManager, IBulletSpawner bulletSpawner)
        {
            _sfxManager = sfxManager;
            _bulletSpawner = bulletSpawner;
        }

        private void SetWeaponData()
        {
            _currentMagazineAmmo = weaponData.currentMagazineAmmo;
            _ammo = weaponData.maxAmmo;
        }

        private void Awake()
        {
            _canShoot = true;
            
            SetWeaponData();
        }

        public void Reload()
        {
            if (_ammo <= 0)
            {
                _sfxManager.MakeSound(weaponData.emptyMagazineSound);
                return;
            }
            if (_currentMagazineAmmo < weaponData.maxMagazineAmmo)
            {
                if (_isReloading == false)
                {
                    _isReloading = true;
                    _sfxManager.MakeSound(weaponData.reloadSound);
                    
                    Timer.StartTimer(weaponData.reloadTime, ReloadingFinished);
                    OnReload.Invoke();
                }
            }
        }

        private void ReloadingFinished()
        {
            UpdateWeaponAmmo();
            _isReloading = false;
            
            OnReloaded?.Invoke();
        }

        private void UpdateWeaponAmmo()
        {
            if (_currentMagazineAmmo > 0)
            {
                _ammo -= weaponData.maxMagazineAmmo - _currentMagazineAmmo;
                _currentMagazineAmmo = weaponData.maxMagazineAmmo;
            }
            else
            {
                if (_ammo <=  weaponData.maxMagazineAmmo)
                {
                    _currentMagazineAmmo = _ammo;
                    _ammo = 0;
                }
                else
                {
                    _currentMagazineAmmo = weaponData.maxMagazineAmmo;
                    _ammo -= weaponData.maxMagazineAmmo;
                }
            }
        }
        
        public void Shoot()
        {
            if (_canShoot)
            {
                if (_currentMagazineAmmo > 0 && _isReloading == false)
                {
                    _sfxManager.MakeSound(weaponData.shotSound);
                    _bulletSpawner.SpawnBullet(transform.position, transform.rotation, weaponData.damage);
                    _currentMagazineAmmo -= 1;

                    _canShoot = false;
                    Timer.StartTimer(weaponData.minTimeBetweenFire , () => _canShoot = true);

                    OnShot.Invoke();
                }
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive); 
        }

        public void SetRotation(Quaternion rotation)
        {
            var targetRotation = rotation; 
            transform.rotation = Quaternion.Lerp(transform.rotation , targetRotation, SmoothRotationTime * Time.deltaTime);
        }

        public void SetPosition(Vector3 position)
        {
            var targetPosition = position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, SmoothPositionTime * Time.deltaTime);
        }

        public void SetPositionImmediately(Vector3 position)
        {
            transform.position = position;
        }

        public void AddAmmoToWeapon(int count)
        {
            _ammo += count;
            OnAddedAmmo?.Invoke();
        }

        public int GetCurrentMagazineAmmo()
        {
            return _currentMagazineAmmo;
        }

        public int GetAmmo()
        {
            return _ammo;
        }

        public Transform GetLeftHandTransform()
        {
            return leftHand;
        }

        public Transform GetRightHandTransform()
        {
            return rightHand;
        }
        
        public WeaponData GetWeaponData()
        {
            return weaponData;
        }

        public WeaponType GetWeaponType()
        {
            return weaponData.weaponType;
        }
    }
}
