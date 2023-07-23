using Scenes.Menu.Scripts;
using UnityEngine;
using Weapons.Scripts;
public class StatsShower : MonoBehaviour
{
    [SerializeField] private SliderScript damageSlider;
    [SerializeField] private SliderScript ammoSlider;
    [SerializeField] private SliderScript rapiditySlider;
    [SerializeField] private SliderScript reloadTimeSlider;
    
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
        damageSlider.SetSliderValue(_weaponData.damage);
        ammoSlider.SetSliderValue(_weaponData.currentMagazineAmmo);
        rapiditySlider.SetSliderValue(_weaponData.minTimeBetweenFire);
        reloadTimeSlider.SetSliderValue(_weaponData.reloadTime);
    }
}
