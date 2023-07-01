using System;
using Player.Scripts;
using TMPro;
using UnityEngine;
using Weapons.Scripts.WeaponBase;
using Zenject;

namespace Weapons.Scripts.UI
{
    public class WeaponAmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoTextObj;
        private PlayerController _playerController;
        
        
        [Inject]
        private void Constructor(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        
        private void OnEnable()
        {
            WeaponSwitcher.OnWeaponSwitched += UpdateAmmoCount;
            Weapon.OnShot += UpdateAmmoCount;
            Weapon.OnReloaded += UpdateAmmoCount;
            Weapon.OnAddedAmmo += UpdateAmmoCount;
        }


        private void Start()
        {
            UpdateAmmoCount();
        }

        private void UpdateAmmoCount()
        {
            var magazineAmmo = _playerController.CurrentWeapon.GetCurrentMagazineAmmo();
            var ammo = _playerController.CurrentWeapon.GetAmmo();

            ammoTextObj.text = magazineAmmo + "/" + ammo;
        }

        private void OnDisable()
        {
            WeaponSwitcher.OnWeaponSwitched -= UpdateAmmoCount;
            Weapon.OnShot -= UpdateAmmoCount;
            Weapon.OnReloaded -= UpdateAmmoCount;
            Weapon.OnAddedAmmo -= UpdateAmmoCount;
        }
    }
}
