using System;
using Common.CommonScripts;
using Player.Scripts;
using UnityEngine;
using Zenject;

namespace Player.PlayerCamera
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private LayerMask fadersLayerMask;
        private ObjectFader _lastObjectFader;
        private RaycastHit _lastRayCastHit;
        private GameObject _player;

        [Inject]
        private void Constructor(PlayerController controller)
        {
            _player = controller.gameObject;
        }
        
        
        private void Update()
        {
            CheckFadeObject();
        }

        private void CheckFadeObject()
        {
            Vector3 dir = _player.gameObject.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit , 50f, fadersLayerMask))
            {
                if (!_lastRayCastHit.Equals(hit))
                {
                    _lastObjectFader = hit.collider.gameObject.GetComponent<ObjectFader>();
                }
                _lastObjectFader.DoFade(true);

                _lastRayCastHit = hit;
            }
            else
            {
                if (_lastObjectFader != null)
                {
                    _lastObjectFader.DoFade(false);
                }
            }

        }

    }
    
}
