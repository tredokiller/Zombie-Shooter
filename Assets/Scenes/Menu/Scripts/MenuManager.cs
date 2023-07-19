using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;
        private WeaponType _currentWeaponType;
        private IWeapon[] _playerWeapons;

        public static Action<WeaponType> OnWeaponTypeChange;

        private MenuStates _currentMenuState = MenuStates.Standard;
        
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

        public void SaveWeapons()
        {
            _playerWeapons = weaponHandler.GetWeapons();
        }

        public void SetWeaponChoosingState()
        {
            _currentMenuState = MenuStates.WeaponPicking;
        }
    }