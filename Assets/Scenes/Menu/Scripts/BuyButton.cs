using System;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Scenes.Menu.Scripts
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private WeaponHandler weaponHandler;
        [SerializeField] private MenuManager menuManager;

        private IGameManager _gameManager;
        
        private Button _button;
        [SerializeField] private TextMeshProUGUI buttonText;
        private const string BuyButtonText = "BUY";
        private const string EquipButtonText = "EQUIP";
        private const string EquippedButtonText = "EQUIPPED";


        [Header("WeaponCost")] 
        [SerializeField] private TextMeshProUGUI weaponCostText;

        [SerializeField] private GameObject weaponCostTextParent;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        [Inject]
        private void Constructor(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(menuManager.TryToBuyAndEquipWeapon);
            WeaponHandler.OnWeaponTriggered += UpdateButton;
        }


        private void UpdateButton()
        {
            ChangeButtonText();
        }
        
        private void ChangeButtonText()
        {
            var currentDisplayedWeapon = weaponHandler.CurrentDisplayedWeapon;
            var weaponData = currentDisplayedWeapon.GetWeaponData();
            if (weaponData.isPurchased)
            {
                weaponCostTextParent.gameObject.SetActive(false);
                if (_gameManager.GetSelectedWeaponsData().Contains(currentDisplayedWeapon.GetWeaponData()))
                {
                    buttonText.text = EquippedButtonText;
                    _button.interactable = false;
                }
                else
                {
                    buttonText.text = EquipButtonText;
                    _button.interactable = true;
                }
            }
            else
            {
                weaponCostTextParent.gameObject.SetActive(true);
                weaponCostText.text = weaponData.weaponPrice.ToString();
                
                buttonText.text = BuyButtonText;

                if (_gameManager.GetMoney() >= weaponData.weaponPrice)
                {
                    _button.interactable = true; 
                    return;
                }
                _button.interactable = false;
            }
        }

        private void OnDisable()
        {
            WeaponHandler.OnWeaponTriggered -= UpdateButton;
        }
    }
}
