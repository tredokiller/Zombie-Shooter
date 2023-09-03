using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private GameInput _gameInput;
        private GameInput.PlayerActions _playerActions;

        private void Awake()
        {
            _gameInput = new GameInput();
            _playerActions = _gameInput.Player;
        }

        private void OnEnable()
        {
            _gameInput.Enable();
        }

        public GameInput.PlayerActions GetPlayerActions()
        {
            return _playerActions;
        }

        private void OnDisable()
        {
            _gameInput.Disable();
        }
    }
}
