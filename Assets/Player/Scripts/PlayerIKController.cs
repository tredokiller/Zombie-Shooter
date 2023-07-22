using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Common.CommonScripts;
using Common.CommonScripts.States;
using Managers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;
using Weapons.Scripts;
using Zenject;
using Random = UnityEngine.Random;
using Timer = Common.CommonScripts.Timer;

namespace Player.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerIKController : MonoBehaviour
    {
        [SerializeField] private Transform leftHandPosition;
        [SerializeField] private Transform rightHandPosition;

        [SerializeField] private Transform[] foots;

        [SerializeField] private LayeredAudioClips[] footStepsDictionary;

        private const float DistanceToGroundFromFoot = 0.35f;
        private const float MinDurationBetweenStepSounds = 0.3f;
        
        private PlayerController _playerController;
        private ISfxManager _sfxManager;

        private bool _canProduceFootstepSound = true;
        private bool _canCheckFootSteps = true;

        [Inject]
        private void Constructor(ISfxManager sfxManager)
        {
            _sfxManager = sfxManager;
        }

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            WeaponSwitcher.OnWeaponSwitched += UpdateHandsPosition;
            PlayerController.OnDied += () => _canCheckFootSteps = false;
        }

        private void Update()
        {
            UpdateHandsPosition();
            
            if (_canCheckFootSteps)
            {
                CheckFootSteps();
            }
        }
        
        private void PlayFootstepSound(RaycastHit groundHit)
        {
            var currentLayer = groundHit.collider.gameObject.layer;
            foreach (var layeredAudioClip in footStepsDictionary)
            {
                if ((layeredAudioClip.LayerMask & (1 << currentLayer)) != 0)
                {
                    int randomIndex = Random.Range(0, layeredAudioClip.AudioClips.Length);
                    _sfxManager.MakeSound(layeredAudioClip.AudioClips[randomIndex] , 0.15f);
                }
            }
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

        private void CheckFootSteps()
        {
            RaycastHit hit;
            if (_playerController.GetState() != State.Idle && _canProduceFootstepSound)
            {
                foreach (var foot in foots)
                {
                    if (Physics.Raycast(foot.transform.position, Vector3.down, out hit, DistanceToGroundFromFoot))
                    {
                        PlayFootstepSound(hit);
                        _canProduceFootstepSound = false;
                        Timer.StartTimer(MinDurationBetweenStepSounds, () => _canProduceFootstepSound = true);
                    } 
                }
            }
        }
        
        private void OnDisable()
        {
            WeaponSwitcher.OnWeaponSwitched -= UpdateHandsPosition;
            PlayerController.OnDied -= () => _canCheckFootSteps = false;
        }
    }
}