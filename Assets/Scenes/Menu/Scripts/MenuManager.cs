using System;
using Scenes.Menu.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;
        [SerializeField] private TextMeshProUGUI buyButton;
        
        private WeaponType _currentWeaponType;
        private IWeapon[] _playerWeapons;

        public static Action OnWeaponScroll;
        public static Action<WeaponType> OnWeaponTypeChange;
        public static Action<int> OnWeaponBuyAndEquip;
        public static Action<int> OnPlayerCoinsChange;
        public int PlayerCoins { get; private set; } = 250;

        private const string BuyButtonText = "Purchase";
        private const string EquipButtonText = "Equip";

        private void OnEnable()
        {
            OnPlayerCoinsChange += SetCoinsValue;
            WeaponHandler.OnIsPurchasedState += ManageBuyButton;
        }

        private void OnDisable()
        {
            OnPlayerCoinsChange -= SetCoinsValue;
            WeaponHandler.OnIsPurchasedState -= ManageBuyButton;
        }

        private void SetCoinsValue(int newValue)
        {
            PlayerCoins = newValue;
        }

        private string SetBuyButtonText(bool isPurchased)
        {
            return isPurchased ? EquipButtonText : BuyButtonText;
        }

        private void ManageBuyButton(bool isPurchased)
        {
            buyButton.text = SetBuyButtonText(isPurchased);
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
            OnWeaponScroll.Invoke();
        }

        public void SwitchPrevWeapon()
        {
            weaponHandler.PrevWeapon();
            OnWeaponScroll.Invoke();
        }

        public void SetPrimaryWeaponType()
        {
            _currentWeaponType = WeaponType.Primary;
            OnWeaponTypeChange.Invoke(WeaponType.Primary);
            OnWeaponScroll.Invoke();
        }
        
        public void SetSecondaryWeaponType()
        {
            _currentWeaponType = WeaponType.Secondary;
            OnWeaponTypeChange.Invoke(_currentWeaponType);
            OnWeaponScroll.Invoke();
        }

        public void TryToBuyAndEquipWeapon()
        {
            OnWeaponBuyAndEquip.Invoke(PlayerCoins);
        }

        public void SaveWeapons()
        {
            _playerWeapons = weaponHandler.GetWeapons();
        }

        public void SellWeapon()
        {
            weaponHandler.PurchaseOrSellWeapon(false);
            OnWeaponScroll.Invoke();
        }
        #endregion
    }