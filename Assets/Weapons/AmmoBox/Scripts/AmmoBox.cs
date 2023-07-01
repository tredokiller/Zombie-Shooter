using System;
using Common.CommonScripts;
using Player.Scripts;
using UnityEngine;
using Zenject;

namespace Weapons.AmmoBox.Scripts
{
    public class AmmoBox : MonoBehaviour
    {
        private PlayerController _controller;

        [NonSerialized] public  int AmmoInBox = 50;

        [Inject]
        private void Constructor(PlayerController controller)
        {
            _controller = controller;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.PlayerTag))
            {
                _controller.CurrentWeapon.AddAmmoToWeapon(AmmoInBox);
                Destroy(gameObject);
            }
        }
    }
}
