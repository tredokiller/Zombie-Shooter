using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;

    public class NewMenuManager : MonoBehaviour
    {
        [SerializeField] private WeaponHandler _weaponHandler;
        private WeaponType _currentWeaponType;

        public static Action<WeaponType> OnWeaponTypeChange; 

        public void StartGame()
        {
            SceneManager.LoadScene(global::Scenes.Scenes.Playground.ToString());
        }

        public void SwitchNextWeapon()
        {
            _weaponHandler.NextWeapon();
        }

        public void SwitchPrevWeapon()
        {
            _weaponHandler.PrevWeapon();
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
    }