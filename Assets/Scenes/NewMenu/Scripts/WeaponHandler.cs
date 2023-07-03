using UnityEngine;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon[] secondaryWeapons;
    [SerializeField] private Weapon[] primaryWeapons;
    
    private IWeapon _currentPrimary;
    private int _currentPrimaryIndex;
    
    private IWeapon _currentSecondary;
    private int _currentSecondaryIndex;

    private void Awake()
    {
        SetBasicWeapons();
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

    public void DisplayNewPrimary()
    {
        
    }
    // private void TryToSwitchWeapon()
    // {
    //     if (weapons.Length > 0)
    //     {
    //         _currentWeaponIndex++;
    //
    //         if (_currentWeaponIndex >= weapons.Length)
    //         {
    //             _currentWeaponIndex = 0;
    //         }
    //         SetCurrentWeapon();
    //     }
    // }

    public void SetNewPrimary(IWeapon newWeapon)
    {
        if (_currentPrimary != null)
            if (_currentPrimary != newWeapon)
                _currentPrimary.SetActive(false);
        
        _currentPrimary = newWeapon;
        _currentPrimary.SetActive(true);
    }
    
    public void DisplayNewSecondary(IWeapon newWeapon)
    {
        if (_currentPrimary != null)
            if (_currentPrimary != newWeapon)
                _currentPrimary.SetActive(false);
        
        _currentPrimary = newWeapon;
        _currentPrimary.SetActive(true);
        
    }
}
