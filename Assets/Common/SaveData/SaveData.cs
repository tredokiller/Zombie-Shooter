using System;

namespace Common.SaveData
{
    [Serializable]
    public class SaveData
    {
        public PlayerSaveData playerSaveData;
        public WeaponSaveData[] weaponsSaveData;

        public SaveData(PlayerSaveData playerSaveData, WeaponSaveData[] weaponSaveData)
        {
            this.playerSaveData = playerSaveData;
            weaponsSaveData = weaponSaveData;
        }
    }
}