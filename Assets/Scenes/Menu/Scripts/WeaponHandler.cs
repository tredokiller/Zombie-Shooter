using System;
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

        private IGameManager _gameManager;
        
        private IWeapon _currentDisplayedWeapon;
        
        private IWeapon _currentPrimary;
        private int _currentPrimaryIndex;

        private IWeapon _currentSecondary;
        private int _currentSecondaryIndex;

        private WeaponType _currentWeaponType;

        public static Action<WeaponData> OnWeaponStatsChange; 
        public static Action<bool> OnIsPurchasedState; 

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }
        private void Awake()
        {
            _currentWeaponType = WeaponType.Primary;
            SetBasicWeapons();
        }
        private void OnEnable()
        {
            MenuManager.OnWeaponTypeChange += SetWeaponType;
            MenuManager.OnWeaponBuyAndEquip += TryToBuyWeapon;
            MenuManager.OnWeaponScroll += CheckIsPurchasedState;
        }
        private void OnDisable()
        {
            MenuManager.OnWeaponTypeChange -= SetWeaponType;
            MenuManager.OnWeaponBuyAndEquip -= TryToBuyWeapon;
            MenuManager.OnWeaponScroll -= CheckIsPurchasedState;
            
        }
        private void SetWeaponType(WeaponType weaponType)
        {
            _currentWeaponType = weaponType;
            
            _currentDisplayedWeapon = weaponType == WeaponType.Primary
                ? _currentPrimary
                : _currentSecondary;
            
            PushWeaponData(_currentDisplayedWeapon.GetWeaponData());
        }
        private void PushWeaponData(WeaponData weaponData)
        {
            OnWeaponStatsChange.Invoke(weaponData);
        }
        private void SetBasicWeapons()
        {
            if (secondaryWeapons == null || primaryWeapons == null) return;
            
            _currentPrimary = primaryWeapons[0];
            _currentSecondary = secondaryWeapons[0];
            _currentPrimary.SetActive(true);
            _currentSecondary.SetActive(true);
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
            _currentDisplayedWeapon = weaponType == WeaponType.Primary ? _currentPrimary : _currentSecondary;
        
            _currentDisplayedWeapon.SetActive(false);
        
            _currentDisplayedWeapon = weapons[weaponIndex];
        
            if (weaponType == WeaponType.Primary)
                _currentPrimary = weapons[weaponIndex];
            else
                _currentSecondary = weapons[weaponIndex];

            _currentDisplayedWeapon.SetActive(true);
            PushWeaponData(_currentDisplayedWeapon.GetWeaponData());
        }
        private void CheckIsPurchasedState()
        {
            OnIsPurchasedState.Invoke(_currentDisplayedWeapon.GetWeaponData().isPurchased);
        }
        private void TryToBuyWeapon(int coins)
        {
            if (_currentDisplayedWeapon.GetWeaponData().isPurchased)
            {
                EquipWeapon();
                return;
            }

            var weaponPrice = _currentDisplayedWeapon.GetWeaponData().weaponPrice;
            
            if (coins < weaponPrice)
            {
                Debug.Log("Not enough money!");
                return;
            }

            PurchaseOrSellWeapon(true);
            _gameManager.AddSubtractMoney(-weaponPrice);
            
            Debug.Log("Purchased successfully!, coins left: " + _gameManager.GetMoney());
            OnIsPurchasedState.Invoke(_currentDisplayedWeapon.GetWeaponData().isPurchased);
            
            EquipWeapon();
        }
        public void PurchaseOrSellWeapon(bool isPurchased)
        {
            _currentDisplayedWeapon.GetWeaponData().isPurchased = isPurchased;
        }
        private void EquipWeapon()
        {
            if (_currentWeaponType == WeaponType.Primary)
                _currentPrimary = _currentDisplayedWeapon;
            else
                _currentSecondary = _currentDisplayedWeapon;
        }
        public IWeapon[] GetWeapons()
        {
            IWeapon[] weapons = { _currentPrimary, _currentSecondary };
            return weapons;
        }
    }
}