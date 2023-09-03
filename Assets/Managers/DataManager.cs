using System.IO;
using System.Linq;

using Common.SaveData;
using UnityEngine;

namespace Managers
{
    public class DataManager : IDataManager
    {
        private readonly GameData _gameData;

        private const string PathName = "SaveData.json";
        private readonly string _saveFilePath;
        
        public DataManager(GameData gameData)
        {
            _gameData = gameData;
            _saveFilePath = Path.Combine(Application.persistentDataPath, PathName);
        }
        
        public void SaveGameData()
        {
            var newPlayerData = GetPlayerData();
            var newWeaponsData = GetWeaponsData();

            var newSaveData = new SaveData(newPlayerData, newWeaponsData);
            string jsonData = JsonUtility.ToJson(newSaveData);
            File.WriteAllText(_saveFilePath, jsonData);

        }

        private PlayerSaveData GetPlayerData()
        {
            var newPlayerSaveData = new PlayerSaveData(_gameData.PlayerMoney,
                _gameData.CurrentPrimaryWeapon.GetWeaponSaveData(),
                _gameData.CurrentSecondaryWeapon.GetWeaponSaveData());

            return newPlayerSaveData;
        }

        private WeaponSaveData[] GetWeaponsData()
        {
            return _gameData.GetAllWeapons().Select(data => data.GetWeaponSaveData()).ToArray();
        }

        public void LoadGameData()
        {
            if (File.Exists(_saveFilePath))
            {
                string jsonData = File.ReadAllText(_saveFilePath);
                _gameData.saveData = JsonUtility.FromJson<SaveData>(jsonData);
            }
            else
            {
                SaveGameData();
            }
            _gameData.InterpretSaveDataToUsableData();
        }
    }
}
