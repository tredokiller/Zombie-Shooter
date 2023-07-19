using System;
using UnityEngine;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;

public class WeaponHandler : MonoBehaviour
{
    [Header("For weapons")]
    [SerializeField] private Weapon[] primaryWeapons;
    [SerializeField] private Weapon[] secondaryWeapons;
    
    private IWeapon _currentPrimary;
    private int _currentPrimaryIndex;

    private IWeapon _currentSecondary;
    private int _currentSecondaryIndex;

    private WeaponType _currentWeaponType;

    public static Action<WeaponData> OnWeaponStatsChange; 

    private void Awake()
    {
        _currentWeaponType = WeaponType.Primary;
        SetBasicWeapons();
    }

    private void OnEnable()
    {
        MenuManager.OnWeaponTypeChange += type => SetWeaponType(type);
        
    }

    private void OnDisable()
    {
        MenuManager.OnWeaponTypeChange -= type => SetWeaponType(type);
    }

    private void SetWeaponType(WeaponType weaponType)
    {
        _currentWeaponType = weaponType;
    }

    private void SetBasicWeapons()
    {
        if (secondaryWeapons != null && primaryWeapons != null)
        {
            _currentPrimary = primaryWeapons[0];
            _currentSecondary = secondaryWeapons[0];
            _currentPrimary.SetActive(true);
            _currentSecondary.SetActive(true);
        }
    }
    public void GetBasicWeaponData()
    {
        OnWeaponStatsChange.Invoke(_currentPrimary.GetWeaponData());
    }
    public void NextWeapon()
    {
        if (_currentWeaponType == WeaponType.Primary)
            TryToSwitchWeapon(WeaponType.Primary, ref _currentPrimaryIndex, true);
        else
            TryToSwitchWeapon(WeaponType.Secondary, ref _currentSecondaryIndex, true);
    }

    public void PrevWeapon()
    {
        if (_currentWeaponType == WeaponType.Primary)
            TryToSwitchWeapon(WeaponType.Primary, ref _currentPrimaryIndex, false);
        else
            TryToSwitchWeapon(WeaponType.Secondary, ref _currentSecondaryIndex, false);
    }

    private void TryToSwitchWeapon(WeaponType weaponType, ref int weaponIndex, bool isNext)
    {
        var weapons = weaponType == WeaponType.Primary ? primaryWeapons : secondaryWeapons;

        if (isNext && weapons.Length > 0)
        {
            weaponIndex++;
            if (weaponIndex >= weapons.Length)
            {
                weaponIndex = 0;
            }
        }
        else if (!isNext && weapons.Length > 0)
        {
            if (weaponIndex <= 0)
            {
                weaponIndex = weapons.Length;
            }
            weaponIndex--;
        }
        SetCurrentWeapon(weaponType, weapons, weaponIndex);
    }

    private void SetCurrentWeapon(WeaponType weaponType, Weapon[] weapons, int weaponIndex)
    {
        var currentWeapon = weaponType == WeaponType.Primary ? _currentPrimary : _currentSecondary;
        
        currentWeapon.SetActive(false);
        
        currentWeapon = weapons[weaponIndex];
        
        if (weaponType == WeaponType.Primary)
            _currentPrimary = weapons[weaponIndex];
        else
            _currentSecondary = weapons[weaponIndex];

        currentWeapon.SetActive(true);
        OnWeaponStatsChange.Invoke(currentWeapon.GetWeaponData());
    }

    public IWeapon[] GetWeapons()
    {
        IWeapon[] weapons = { _currentPrimary, _currentSecondary };
        return weapons;
    }
}