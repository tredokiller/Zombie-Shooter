using Cinemachine;
using UnityEngine;
using Weapons.Scripts;
using Weapons.Scripts.WeaponBase;

namespace Player.PlayerCamera.CameraShake
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource cmImpulseSource;
        private void OnEnable()
        {
            Weapon.OnShot += Shake;
        }
        
        private void OnDisable()
        {
            Weapon.OnShot -= Shake;
        }
        
        private void Shake()
        {
            cmImpulseSource.GenerateImpulse();
        }
    }
}
