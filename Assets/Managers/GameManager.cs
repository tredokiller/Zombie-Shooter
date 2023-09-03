using System;
using Common.SaveData;
using UnityEngine;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;

namespace Managers
{
    public class GameManager : MonoBehaviour , IGameManager
    {
        [SerializeField] private GameData gameData;

        private IDataManager _dataManager;

        private WeaponData CurrentPrimaryWeaponData
        {
            set => gameData.CurrentPrimaryWeapon = value;
            get => gameData.CurrentPrimaryWeapon;
        }

        private WeaponData CurrentSecondaryWeaponData
        {
            set => gameData.CurrentSecondaryWeapon = value;
            get => gameData.CurrentSecondaryWeapon;
        }

        private int PlayerMoney
        {
            set => gameData.PlayerMoney = value;
            get => gameData.PlayerMoney;
        }
        
        public static Action OnMoneyChanged;
        public static Action OnWeaponChanged;
        

        private void Awake()
        {
            _dataManager = new DataManager(gameData);
            LoadGame();
            
            SetFrameRate();
        }

        public WeaponData[] GetSelectedWeaponsData()
        {
            return new []{CurrentPrimaryWeaponData, CurrentSecondaryWeaponData};
        }

        public WeaponData GetPrimaryWeaponData()
        {
            return CurrentPrimaryWeaponData;
        }

        public WeaponData GetSecondaryWeaponData()
        {
            return CurrentSecondaryWeaponData;
        }

        public bool AreWeaponsSelected()
        {
            if (CurrentPrimaryWeaponData == null || CurrentSecondaryWeaponData == null)
            {
                return false;
            }

            return true;
        }

        public void SetWeaponToSelectedList(IWeapon newSelectedWeapon)
        {
            
            if (CurrentPrimaryWeaponData == newSelectedWeapon.GetWeaponData() || CurrentSecondaryWeaponData == newSelectedWeapon.GetWeaponData())
            {
                return;
            }

            OnWeaponChanged?.Invoke();
            
            if (newSelectedWeapon.GetWeaponType() == WeaponType.Primary)
            {
                CurrentPrimaryWeaponData = newSelectedWeapon.GetWeaponData();
            }
            else
            {
                CurrentSecondaryWeaponData = newSelectedWeapon.GetWeaponData();
            }
        }

        public int GetMoney()
        {
            return PlayerMoney;
        }

        public void AddSubtractMoney(int countOfMoney)
        {
            PlayerMoney += countOfMoney;
            OnMoneyChanged?.Invoke();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }

        public void SaveGame()
        {
            _dataManager.SaveGameData();
        }

        public void LoadGame()
        {
            _dataManager.LoadGameData();
        }
        
        private void SetFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void OnDisable()
        {
            SaveGame();
        }
    }
}
