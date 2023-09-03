using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;
using Zenject;

namespace Scenes.Menu.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;

        private WeaponType _currentWeaponType;
        private IWeapon[] _playerWeapons;

        public static Action OnWeaponTriggered;
        public static Action<WeaponType> OnWeaponTypeChange;
        public static Action OnTryWeaponBuyAndEquip;

        #region ButtonLinksMethods
        
        public void StartGame()
        {
            Initiate.Fade(Scenes.Playground.ToString() , Color.black);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void SwitchNextWeapon()
        {
            weaponHandler.NextWeapon();
            OnWeaponTriggered.Invoke();
        }

        public void SwitchPrevWeapon()
        {
            weaponHandler.PrevWeapon();
            OnWeaponTriggered.Invoke();
        }

        public void SetPrimaryWeaponType()
        {
            _currentWeaponType = WeaponType.Primary;
            OnWeaponTypeChange.Invoke(WeaponType.Primary);
            OnWeaponTriggered.Invoke();
        }
        
        public void SetSecondaryWeaponType()
        {
            _currentWeaponType = WeaponType.Secondary;
            OnWeaponTypeChange.Invoke(_currentWeaponType);
            OnWeaponTriggered.Invoke();
        }

        public void TryToBuyAndEquipWeapon()
        {
            OnTryWeaponBuyAndEquip.Invoke();
        }
        #endregion
    }
}