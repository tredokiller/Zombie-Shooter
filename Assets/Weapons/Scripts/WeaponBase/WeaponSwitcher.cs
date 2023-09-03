using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Zenject;

namespace Weapons.Scripts.WeaponBase
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        
        private InputManager _inputManager;
        private IGameManager _gameManager;
        private DiContainer _diContainer;
        
        private GameInput.PlayerActions _playerActions;

        public static Action OnWeaponSwitched;


        private void Awake()
        { 
            LoadWeapons();
        }

        [Inject]
        private void Constructor(InputManager inputManager , IGameManager gameManager , DiContainer diContainer)
        {
            _inputManager = inputManager;
            _gameManager = gameManager;
            _diContainer = diContainer;
            
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

        private void LoadWeapons() //Loading weapons from GameManager
        {
            var loadedWeaponDates = _gameManager.GetSelectedWeaponsData();
            List<Weapon> newWeaponsList = new List<Weapon>();
            foreach (var weaponData in loadedWeaponDates)
            {
                var weapon=_diContainer.InstantiatePrefab(weaponData.weaponPrefab);
                weapon.SetActive(false);
                
                newWeaponsList.Add(weapon.GetComponent<Weapon>());
            }

            weapons = newWeaponsList.ToArray();
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
