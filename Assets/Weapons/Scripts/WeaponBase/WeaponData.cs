using System;
using Common.SaveData;
using UnityEngine;

namespace Weapons.Scripts.WeaponBase
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public WeaponType weaponType;
        public WeaponName weaponName;

        public GameObject weaponPrefab;
        
        public int weaponPrice;
        public bool isPurchased = false;
        
        [Range(50 , 250)] public int maxAmmo = 100;
        
        [Range(5 , 50)]public int maxMagazineAmmo = 10;
        [Range(1 , 10)]public float reloadTime = 2f;
        
        public int currentMagazineAmmo = 5;
        
        [Range(1 , 100)] public float damage = 40;
        [Range(0.05f, 2)] public float minTimeBetweenFire = 0.05f;
        
        public AudioClip shotSound;
        public AudioClip reloadSound;
        public AudioClip emptyMagazineSound;

        public WeaponSaveData GetWeaponSaveData()
        {
            return new WeaponSaveData(weaponName, isPurchased);
        }

        public void SetWeaponSaveData(WeaponSaveData saveData)
        {
            isPurchased = saveData.isPurchased;
        }
    }
}
