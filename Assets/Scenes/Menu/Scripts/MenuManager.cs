using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Menu.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(Scenes.Playground.ToString());
        }
    }
}
