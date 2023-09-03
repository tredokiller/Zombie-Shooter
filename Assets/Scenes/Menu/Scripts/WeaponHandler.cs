using System;
using System.Linq;
using Managers;
using UnityEngine;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;
using Zenject;

namespace Scenes.Menu.Scripts
{
    public class WeaponHandler : MonoBehaviour
    {
        [Header("For weapons")]
        [SerializeField] private Weapon[] primaryWeapons;
        [SerializeField] private Weapon[] secondaryWeapons;
        public IWeapon CurrentDisplayedWeapon { private set; get; }

        private IWeapon _currentPrimary;
        private int _currentPrimaryIndex;

        private IWeapon _currentSecondary;
        private int _currentSecondaryIndex;

        private WeaponType _currentWeaponType;

        public static Action<WeaponData> OnWeaponStatsChange; 
        public static Action OnWeaponTriggered;

        private IGameManager _gameManager;
        
        
        [Inject]
        private void Constructor(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        private void Awake()
        {
            _currentWeaponType = WeaponType.Primary;
            SetBasicWeapons();
        }

        private void OnEnable()
        {
            MenuManager.OnWeaponTypeChange += SetWeaponType;
            MenuManager.OnTryWeaponBuyAndEquip += TryToBuyWeapon;
            MenuManager.OnWeaponTriggered += CheckIsPurchasedState;
        }

        private void OnDisable()
        {
            MenuManager.OnWeaponTypeChange -= SetWeaponType;
            MenuManager.OnTryWeaponBuyAndEquip -= TryToBuyWeapon;
            MenuManager.OnWeaponTriggered -= CheckIsPurchasedState;
            
        }

        
        private void SetBasicWeapons()
        {
            if (_gameManager.AreWeaponsSelected() == false)
            {
                _gameManager.SetWeaponToSelectedList(primaryWeapons.First());
                _gameManager.SetWeaponToSelectedList(secondaryWeapons.First());
            }

            _currentPrimary = GetWeaponFromWeaponListByData(_gameManager.GetPrimaryWeaponData());
            _currentSecondary = GetWeaponFromWeaponListByData(_gameManager.GetSecondaryWeaponData());
            _currentPrimary.SetActive(true);
            _currentSecondary.SetActive(true);
        }

        private IWeapon GetWeaponFromWeaponListByData(WeaponData weaponData)
        {
            Weapon[] weapons = primaryWeapons;
            if (weaponData.weaponType == WeaponType.Primary)
            {
                weapons = primaryWeapons;
            }
            else
            {
                weapons = secondaryWeapons;
            }

            foreach (var weapon in weapons)
            {
                if (weapon.GetWeaponData() == weaponData)
                {
                    return weapon;
                }
            }

            return weapons.First();
        }
        
        private void SetWeaponType(WeaponType weaponType)
        {
            _currentWeaponType = weaponType;
            
            CurrentDisplayedWeapon = weaponType == WeaponType.Primary
                ? _currentPrimary
                : _currentSecondary;
            
            PushWeaponData(CurrentDisplayedWeapon.GetWeaponData());
        }

        private void PushWeaponData(WeaponData weaponData)
        {
            OnWeaponStatsChange.Invoke(weaponData);
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
            CurrentDisplayedWeapon = weaponType == WeaponType.Primary ? _currentPrimary : _currentSecondary;
        
            CurrentDisplayedWeapon.SetActive(false);
        
            CurrentDisplayedWeapon = weapons[weaponIndex];
        
            if (weaponType == WeaponType.Primary)
                _currentPrimary = weapons[weaponIndex];
            else
                _currentSecondary = weapons[weaponIndex];

            CurrentDisplayedWeapon.SetActive(true);
            PushWeaponData(CurrentDisplayedWeapon.GetWeaponData());
        }

        private void CheckIsPurchasedState()
        {
            OnWeaponTriggered.Invoke();
        }
        
        private void TryToBuyWeapon()
        {
            if (CurrentDisplayedWeapon.GetWeaponData().isPurchased)
            {
                EquipWeapon();
                return;
            }

            var weaponPrice = CurrentDisplayedWeapon.GetWeaponData().weaponPrice;
            
            
            if (_gameManager.GetMoney() < weaponPrice)
            {
                Debug.Log("Not enough money!");
                return;
            }
            
            _gameManager.AddSubtractMoney(-weaponPrice);
            PurchaseOrSellWeapon(true);
            
            
            Debug.Log("Purchased successfully!");
            OnWeaponTriggered.Invoke();
            
        }

        private void PurchaseOrSellWeapon(bool isPurchased)
        {
            CurrentDisplayedWeapon.GetWeaponData().isPurchased = isPurchased;
        }
        private void EquipWeapon()
        {
            _gameManager.SetWeaponToSelectedList(CurrentDisplayedWeapon);
            OnWeaponTriggered.Invoke();
        }
    }
}