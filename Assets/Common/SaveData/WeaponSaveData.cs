using System;
using UnityEngine.Serialization;
using Weapons.Scripts.WeaponBase;

namespace Common.SaveData
{
    [Serializable]
    public class WeaponSaveData
    {
        public WeaponName weaponName;
        public bool isPurchased;

        public WeaponSaveData(WeaponName _weaponName, bool isPurchased)
        {
            weaponName = _weaponName;
            this.isPurchased = isPurchased;
        }
    }
}
