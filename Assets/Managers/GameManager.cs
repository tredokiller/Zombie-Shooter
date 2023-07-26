using System;
using UnityEngine;
using Weapons.Scripts;

namespace Managers
{
    public class GameManager : MonoBehaviour , IGameManager
    {
        private IWeapon[] _selectedWeapons;
        private int _playerMoney;
        
        public static Action OnMoneyChanged;
        
        private const string PlayerMoneyKey = "PlayerMoney";
        
        public IWeapon[] GetSelectedWeapons()
        {
            return _selectedWeapons;
        }

        public void SetWeaponsToSelectedList(IWeapon[] newSelectedWeapons)
        {
            _selectedWeapons = newSelectedWeapons;
        }

        public int GetMoney()
        {
            return _playerMoney;
        }

        public void AddSubtractMoney(int countOfMoney)
        {
            _playerMoney += countOfMoney;
            OnMoneyChanged?.Invoke();
        }

        public void SaveGame()
        {
            PlayerPrefs.SetInt(PlayerMoneyKey , _playerMoney);
        }

        public void LoadGame()
        {
            _playerMoney = PlayerPrefs.GetInt(PlayerMoneyKey);
        }
    }
}
