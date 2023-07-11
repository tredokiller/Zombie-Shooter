using UnityEngine;
using Weapons.Scripts;

public class StatsShower : MonoBehaviour
{
    public WeaponData WeaponData { get; private set; }
    public void SetWeaponData(WeaponData weaponData)
    {
        WeaponData = weaponData;
    }
}
