using System;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scenes.Menu.Scripts
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyCountText;

        private IGameManager _gameManager;

        private void Awake()
        {
            UpdateDisplayCountOfMoney();
        }


        [Inject]
        private void Constructor(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnEnable()
        {
            UpdateDisplayCountOfMoney();
            GameManager.OnMoneyChanged += UpdateDisplayCountOfMoney;
        }

        private void UpdateDisplayCountOfMoney()
        {
            moneyCountText.text = _gameManager.GetMoney().ToString();
        }
        
        private void OnDisable()
        {
            GameManager.OnMoneyChanged -= UpdateDisplayCountOfMoney;
        }
    }
}
