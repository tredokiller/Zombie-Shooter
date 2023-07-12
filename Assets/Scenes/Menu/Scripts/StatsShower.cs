using System;
using UnityEngine;
using Weapons.Scripts;
public class StatsShower : MonoBehaviour
{
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private SliderScript damageSlider;
    private WeaponData _weaponData;
    
    
    private void OnEnable()
    {
        WeaponHandler.OnWeaponStatsChange += data => SetWeaponData(data);
    }
    
    private void OnDisable()
    {
        WeaponHandler.OnWeaponStatsChange -= data => SetWeaponData(data);
    }

    private void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;
        SetSlidersValues();
    }

    private void SetSlidersValues()
    {
        if (damageSlider != null)
            damageSlider.SetSliderValue(_weaponData.damage);
    }
}
