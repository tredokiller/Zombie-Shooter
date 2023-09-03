using System;
using UnityEngine;
using Weapons.Scripts.WeaponBase;

namespace Common.SaveData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData")]
    public class GameData : ScriptableObject
    {
        public SaveData saveData;
        
        public int PlayerMoney = 500;

        [NonSerialized] public WeaponData CurrentPrimaryWeapon;
        [NonSerialized] public WeaponData CurrentSecondaryWeapon;

        [SerializeField] private WeaponData[] allWeapons;

        public WeaponData[] GetAllWeapons()
        {
            return allWeapons;
        }

        public void InterpretSaveDataToUsableData()
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if (allWeapons[i].weaponName == saveData.playerSaveData.currentPrimarySaveData.weaponName)
                {
                    CurrentPrimaryWeapon = allWeapons[i];
                }
                else if (allWeapons[i].weaponName == saveData.playerSaveData.currentSecondarySaveData.weaponName)
                {
                    CurrentSecondaryWeapon = allWeapons[i];
                }
                
                allWeapons[i].SetWeaponSaveData(saveData.weaponsSaveData[i]);
            }
        
            PlayerMoney = saveData.playerSaveData.GetMoney();
        }

    }
}
