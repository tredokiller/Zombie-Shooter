using System;
using Player.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player.UI.Health
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Image hpImage;
        private PlayerController _playerController;
        
        [Inject]
        private void Constructor(PlayerController player)
        {
            _playerController = player;
        }

        private void Start()
        {
            UpdateHp();
        }

        private void OnEnable()
        {
            PlayerController.OnDamaged += UpdateHp;
        }

        private void UpdateHp()
        {
            hpImage.fillAmount = _playerController.Health / _playerController.MaxHealth;
        }

        private void OnDisable()
        {
            PlayerController.OnDamaged -= UpdateHp;
        }
    }
}
