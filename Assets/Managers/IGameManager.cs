using Weapons.Scripts.WeaponBase;

namespace Managers
{
    public interface IGameManager
    {
        public WeaponData[] GetSelectedWeaponsData();
        public WeaponData GetPrimaryWeaponData();
        public WeaponData GetSecondaryWeaponData();

        public bool AreWeaponsSelected();
        
        public void SetWeaponToSelectedList(IWeapon newSelectedWeapons); //Add Weapons to selected list 
        
        public int GetMoney();
        public void AddSubtractMoney(int count);

        public void PauseGame();

        public void ResumeGame();

        public void SaveGame();
        public void LoadGame();
    }
}