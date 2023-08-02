using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;
using Zenject;

namespace Scenes.Menu.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private WeaponHandler weaponHandler;
        [SerializeField] private TextMeshProUGUI buyButton;
        [SerializeField] private TextMeshProUGUI displayedCoins;
        
        private WeaponType _currentWeaponType;
        private IWeapon[] _playerWeapons;

        public static Action OnWeaponScroll;
        public static Action<WeaponType> OnWeaponTypeChange;
        public static Action<int> OnWeaponBuyAndEquip;

        private const string BuyButtonText = "Purchase";
        private const string EquipButtonText = "Equip";
        
        private IGameManager _gameManager;

        [Inject]
        private void Constructor(IGameManager gameManager)
        {
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        private void Awake()
        {
            ManageDisplayedCoins();
        }

        private void OnEnable()
        {
            WeaponHandler.OnIsPurchasedState += ManageBuyButton;
            GameManager.OnMoneyChanged += ManageDisplayedCoins;
        }
        private void OnDisable()
        {
            WeaponHandler.OnIsPurchasedState -= ManageBuyButton;
            GameManager.OnMoneyChanged -= ManageDisplayedCoins;
        }
        private string SetBuyButtonText(bool isPurchased)
        {
            return isPurchased ? EquipButtonText : BuyButtonText;
        }
        private void ManageBuyButton(bool isPurchased)
        {
            buyButton.text = SetBuyButtonText(isPurchased);
        }

        private void ManageDisplayedCoins()
        {
            displayedCoins.text = _gameManager.GetMoney().ToString();
        }
        #region ButtonLinksMethods
        
        public void StartGame()
        {
            SceneManager.LoadScene(Scenes.Playground.ToString());
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
            OnWeaponBuyAndEquip.Invoke(_gameManager.GetMoney());
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
}