using Weapons.Scripts;

namespace Managers
{
    public interface IGameManager
    {
        public IWeapon[] GetSelectedWeapons();
        public void SetWeaponsToSelectedList(IWeapon[] newSelectedWeapons); //Add Weapons to selected list 
        
        public int GetMoney();
        public void AddSubtractMoney(int count);
        
        public void SaveGame();
        public void LoadGame();
    }
}