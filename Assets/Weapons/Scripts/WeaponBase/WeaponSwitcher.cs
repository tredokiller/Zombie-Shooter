using System;
using Inputs;
using UnityEngine;
using Weapons.Scripts.WeaponBase;
using Zenject;

namespace Weapons.Scripts
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        
        private InputManager _inputManager;
        private GameInput.PlayerActions _playerActions;

        public static Action OnWeaponSwitched;
        

        [Inject]
        private void Constructor(InputManager inputManager)
        {
            _inputManager = inputManager;
            _playerActions = _inputManager.GetPlayerActions();
        }

        private void OnEnable()
        {
            _playerActions.SwitchWeapon.started += context => TryToSwitchWeapon();
        }
        
        private void Start()
        {
            TryToSwitchWeapon();
        }

        private void TryToSwitchWeapon()
        {
            if (weapons.Length > 0)
            {
                _currentWeaponIndex++;

                if (_currentWeaponIndex >= weapons.Length)
                {
                    _currentWeaponIndex = 0;
                }
                SetCurrentWeapon();
            }
        }

        private void SetCurrentWeapon()
        {
            var newWeapon = weapons[_currentWeaponIndex];
            if (_currentWeapon != null)
            {
                if (_currentWeapon != newWeapon)
                {
                    _currentWeapon.SetActive(false);
                }
            }
            _currentWeapon = newWeapon;
            _currentWeapon.SetActive(true);
            
            OnWeaponSwitched.Invoke();
        }

        public IWeapon GetCurrentWeapon()
        {
            return _currentWeapon;
        }

        public void OnDisable()
        {
            _playerActions.SwitchWeapon.started -= context => TryToSwitchWeapon();
        }
    }
}
