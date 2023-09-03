using System;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;

namespace Player.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private PlayerController _controller;
        [SerializeField] private Animator animator;
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int YValue = Animator.StringToHash("YValue");
        private static readonly int XValue = Animator.StringToHash("XValue");
        private static readonly int WeaponType = Animator.StringToHash("WeaponType");
        private static readonly int ShotTrigger = Animator.StringToHash("ShotTrigger");
        private static readonly int ReloadTrigger = Animator.StringToHash("ReloadTrigger");
        
        [SerializeField] private float defaultDampTime = 1f;

        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            WeaponSwitcher.OnWeaponSwitched += UpdateWeapon;
            Weapon.OnShot += SetShotTrigger;
            Weapon.OnReload += SetReloadTrigger;
        }

        private void SetShotTrigger()
        {
            animator.SetTrigger(ShotTrigger);
        }

        private void SetReloadTrigger()
        {
            animator.SetTrigger(ReloadTrigger);
        }
        
        private void Update()
        {
            animator.SetInteger(State , (int)_controller.playerData.state);
            
            animator.SetFloat(XValue , _controller.RotationForAnimationHorizontal.x, defaultDampTime, Time.deltaTime);
            animator.SetFloat(YValue , _controller.RotationForAnimationHorizontal.z , defaultDampTime, Time.deltaTime);
        }

        private void UpdateWeapon()
        {
            animator.SetInteger(WeaponType , (int)_controller.weaponType);
        }
        
        private void OnDisable()
        {
            WeaponSwitcher.OnWeaponSwitched -= UpdateWeapon;
            Weapon.OnShot -= SetShotTrigger;
            Weapon.OnReload -= SetReloadTrigger;
        }

    }
}