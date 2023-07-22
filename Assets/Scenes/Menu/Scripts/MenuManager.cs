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
        public static Action<int> OnWeaponBuyAndEquip;
        public static Action<int> OnPlayerCoinsChange;
        public int PlayerCoins { get; private set; } = 0;

        private void OnEnable()
        {
            OnPlayerCoinsChange += SetCoinsValue;
        }

        private void OnDisable()
        {
            OnPlayerCoinsChange -= SetCoinsValue;
        }

        private void SetCoinsValue(int newValue)
        {
            
        }
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
            OnWeaponBuyAndEquip.Invoke(PlayerCoins);
        }

        public void SaveWeapons()
        {
            _playerWeapons = weaponHandler.GetWeapons();
        }
        
        #endregion
    }