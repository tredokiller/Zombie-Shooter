using UnityEngine;

namespace Managers
{
    public interface IDataManager
    {
        public void SaveGameData();

        public void LoadGameData();
    }
}
