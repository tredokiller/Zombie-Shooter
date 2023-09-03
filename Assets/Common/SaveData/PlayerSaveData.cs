using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Scripts.WeaponBase;
using Zenject;

namespace Common.SaveData
{
    [Serializable]
    public class PlayerSaveData
    {
        [SerializeField] private int playerMoney;

        public WeaponSaveData currentPrimarySaveData;
        public WeaponSaveData currentSecondarySaveData;

        public PlayerSaveData(int money, WeaponSaveData primaryData, WeaponSaveData secondaryData)
        {
            playerMoney = money;
            currentPrimarySaveData = primaryData;
            currentSecondarySaveData = secondaryData;
        }

        public int GetMoney()
        {
            return playerMoney;
        }
    }
}