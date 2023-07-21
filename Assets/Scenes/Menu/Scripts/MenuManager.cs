using System;
using Scenes.Menu.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;
        private WeaponType _currentWeaponType;
        private IWeapon[] _playerWeapons;

        public static Action<WeaponType> OnWeaponTypeChange;

        #region ButtonLinksMethods
        
        public void StartGame()
        {
            SceneManager.LoadScene(Scenes.Scenes.Playground.ToString());
        }
        
        public void QuitGame()
        {
            Application.Quit();   
        }

        public void SwitchNextWeapon()
        {
            weaponHandler.NextWeapon();
        }

        public void SwitchPrevWeapon()
        {
            weaponHandler.PrevWeapon();
        }

        public void SetPrimaryWeaponType()
        {
            _currentWeaponType = WeaponType.Primary;
            OnWeaponTypeChange.Invoke(WeaponType.Primary);
        }
        
        public void SetSecondaryWeaponType()
        {
            _currentWeaponType = WeaponType.Secondary;
            OnWeaponTypeChange.Invoke(_currentWeaponType);
        }

        public void TryToBuyAndEquipWeapon()
        {
            weaponHandler.TryToEquipWeapon();
        }

        public void SaveWeapons()
        {
            _playerWeapons = weaponHandler.GetWeapons();
        }
        
        #endregion
    }