using UnityEngine;
using Weapons.AmmoBox.Scripts;
using Weapons.Scripts;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour , IGameManager
    {
        private IWeapon[] _selectedWeapons;
        private int _playerMoney;
        
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

        public void SaveGame()
        {
            throw new System.NotImplementedException();
        }

        public void LoadGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
