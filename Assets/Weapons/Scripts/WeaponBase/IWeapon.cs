using UnityEngine;
using Weapons.Scripts.WeaponBase;

namespace Weapons.Scripts
{
    public interface IWeapon
    {
        public void Reload();
        public void Shoot();
        public void SetActive(bool isActive);
        public void SetRotation(Quaternion rotation);
        public void SetPosition(Vector3 position);
        public void SetPositionImmediately(Vector3 position);

        public void AddAmmoToWeapon(int count);

        public int GetCurrentMagazineAmmo();

        public int GetAmmo();

        public Transform GetLeftHandTransform();

        public Transform GetRightHandTransform();

        public WeaponData GetWeaponData();
        
        public WeaponType GetWeaponType();
    }
}
