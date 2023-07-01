using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Weapons.Scripts;

namespace Player.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerIKController : MonoBehaviour
    {
        [SerializeField] private Transform leftHandPosition;
        [SerializeField] private Transform rightHandPosition;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            WeaponSwitcher.OnWeaponSwitched += UpdateHandsPosition;
        }

        private void Update()
        {
            UpdateHandsPosition();
        }

        private void UpdateHandsPosition()
        {
            if (_playerController.CurrentWeapon != null)
            {
                var leftHand = _playerController.CurrentWeapon.GetLeftHandTransform();
                var rightHand = _playerController.CurrentWeapon.GetRightHandTransform();

                leftHandPosition.position = leftHand.position;
                leftHandPosition.rotation = leftHand.rotation;

                rightHandPosition.position = rightHand.position;
                rightHandPosition.rotation = rightHand.rotation;
            }
        }
        
        private void OnDisable()
        {
            WeaponSwitcher.OnWeaponSwitched -= UpdateHandsPosition;
        }
    }
}